using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CMSystem;
using Project.Actors;
using Project.Data.CMS.Tags;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.Game.Battle;
using Project.Sound;
using Project.UI;
using GameData;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.GameManagers{
    
    public class BattleManager: MonoBehaviour{
        
        [Inject]
        private void Construct(
            SignalBus signalBus, 
            IEnemyViewFactory enemyFactory,
            IHeroFactory heroFactory,
            GameDataTracker runtimeDataProvider,
            ISaveSystem saveSystem)
        {
            m_SignalBus = signalBus;
            m_EnemyFactory = enemyFactory;
            m_HeroFactory = heroFactory;
            m_RuntimeDataProvider = runtimeDataProvider;

            m_SaveSystem = saveSystem;
        }

        IEnumerator Start()
        {
            yield return PlayMusic();
        }

        void OnEnable()
        {
            m_SignalBus.Subscribe<EnemyDiedSignal>(OnEnemyDied);
            m_SignalBus.Subscribe<HeroDiedSignal>(OnHeroDied);
        }

        void OnDisable()
        {
            m_SignalBus.Unsubscribe<EnemyDiedSignal>(OnEnemyDied);
            m_SignalBus.Unsubscribe<HeroDiedSignal>(OnHeroDied);
        }

        void OnDestroy()
        {
            m_EnemiesSpawnPoints = null;
            m_HeroesSpawnPoints = null;
            
            m_EnemiesInBattle = null;
            m_HeroesInBattle = null;
        }

        private SignalBus m_SignalBus;
        private GameDataTracker m_RuntimeDataProvider;
        private IEnemyViewFactory m_EnemyFactory;
        private IHeroFactory m_HeroFactory;
        private ISaveSystem m_SaveSystem;
        
        // TO REMOVE
        [SerializeField] UINotification uiNotification;
        
        
        [SerializeField] SoundChannel m_MusicChannel;
        [SerializeField] AudioClip m_Music;
        
        [SerializeField] Transform[] m_EnemiesSpawnPoints;
        [SerializeField] Transform[] m_HeroesSpawnPoints;
        
        private IReadOnlyList<CMSEntityPfb> m_EnemiesInBattle; 
        private IReadOnlyList<HeroState> m_HeroesInBattle;


        private List<Hero> m_CurrentHeroesInBattle;
        private List<EnemyView> m_CurrentEnemiesInBattle;
        
        private BattleStage m_CurrentBattleStage;
        private int m_EnemyFightedCounter = 0;
        
        private IEnumerator PlayMusic(){
            while(true){
                m_MusicChannel.SetVolume(0.1f);
                m_MusicChannel.PlaySound(m_Music);

                yield return new WaitForSeconds(m_Music.length - 0.1f);
            }    
        }
        
        public void StartBattle(){

            if (!ConfigureBattleData()){
               m_SaveSystem.CreateNewSaveFile();
               ConfigureBattleData();
            }

            ClearOldHeroesSlots();

            SetupBattleStage();

            NotifyBattleStageReady();
        }
        
        private void NotifyBattleStageReady()
            => m_SignalBus.SendSignal(new BattleStageReadySignal(m_CurrentBattleStage));
        
        private void ClearOldHeroesSlots(){
            if(m_CurrentEnemiesInBattle == null) {return; }
            foreach(var hero in m_CurrentHeroesInBattle){
                Destroy(hero.m_view.gameObject);
            }
        }
        
        private void SetupBattleStage(){

            m_CurrentHeroesInBattle = new();
            m_CurrentEnemiesInBattle = new();
            
            int heroIndex = 0;
            foreach(var model in m_HeroesInBattle){
                var hero = m_HeroFactory.CreateFromHeroState(model, m_HeroesSpawnPoints[heroIndex++]);
                m_CurrentHeroesInBattle.Add(hero);
                m_SignalBus.SendSignal(new HeroSpawnedSignal(hero));
            }
            
            int limit = m_EnemiesSpawnPoints.Length;
            for (int i = m_EnemyFightedCounter; i < m_EnemiesInBattle.Count; i++){
                var enemy = m_EnemyFactory.CreateFromCMS(m_EnemiesInBattle[i], m_EnemiesSpawnPoints[--limit]);
                m_CurrentEnemiesInBattle.Add(enemy);
                m_SignalBus.SendSignal(new EnemySpawnedSignal(enemy));
                if(limit == 0){break;}
            }

            m_CurrentBattleStage = new BattleStage(ref m_CurrentEnemiesInBattle, ref m_CurrentHeroesInBattle);
        }      
               
        private bool ConfigureBattleData(){
            var location = m_RuntimeDataProvider.GetCurrentLocation();
            var dungeon = location.GetTag<TagDungeon>();
            
            if(dungeon == null){throw new System.Exception("Unable to setup battle, because current location have not TagDungeon.");}

            m_EnemiesInBattle = dungeon.GetEnemies();
            m_HeroesInBattle = SaveFile.AccessPlayerData().GetHeroes().Where((h) => h.m_stats.m_BaseStats.m_Health != 0).ToList();
            
            if(m_HeroesInBattle.Count == 0){
                return false;
            }
            else
            {
                return true;
            }
        }
        
        private void OnEnemyDied(EnemyDiedSignal signal){
            if(!m_CurrentBattleStage.GetEnemies().Contains(signal.GetEnemy())){return ;}

            m_EnemyFightedCounter++;
            
            m_CurrentEnemiesInBattle.Remove(signal.GetEnemy());
            Destroy(signal.GetEnemy().gameObject);
            
            if(m_CurrentBattleStage.isCompleted()){
                StartCoroutine(Won());
            }
        }
        
        private void OnHeroDied(HeroDiedSignal signal){
            
            m_CurrentHeroesInBattle.Remove(signal.hero);
                                    
            if(m_CurrentBattleStage.isAllHeroesDied()){
                StartCoroutine(Lost());
            }
        }
        
        
        private IEnumerator Won(){
            
            if(m_EnemyFightedCounter < m_EnemiesInBattle.Count){
                StartBattle();
                yield break;
            }

            yield return m_SaveSystem.SaveData();


            var loading = SceneManager.LoadSceneAsync("MapScene", LoadSceneMode.Single);
            
            loading.allowSceneActivation = false;

            uiNotification.ShowNotification("You won!", Color.green);

            yield return new WaitForSeconds(2.0f);
            
            while (loading.progress < 0.9f){
                yield return null;
            }
            
            loading.allowSceneActivation = true;
        }

        private IEnumerator Lost(){
            var loading = SceneManager.LoadSceneAsync("MapScene", LoadSceneMode.Single);

            loading.allowSceneActivation = false;

            uiNotification.ShowNotification("You lost!", Color.red);

            yield return new WaitForSeconds(2.0f);

            while (loading.progress < 0.9f)
            {
                yield return null;
            }

            loading.allowSceneActivation = true;
        }

    }
    
}
