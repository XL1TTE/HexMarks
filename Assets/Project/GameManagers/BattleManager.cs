using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CMSystem;
using Project.Actors;
using Project.Data.CMS.Tags;
using Project.Data.SaveFile;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.Game.Battle;
using Project.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.GameManagers{
    
    public class BattleManager: MonoBehaviour{
        
        [Inject]
        private void Construct(
            SignalBus signalBus, 
            RuntimeDataProvider dataProvider,
            IEnemyViewFactory enemyFactory,
            IHeroViewFactory heroFactory)
        {
            m_SignalBus = signalBus;
            m_RuntimeDataProvider = dataProvider;
            m_EnemyFactory = enemyFactory;
            m_HeroFactory = heroFactory;
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
        private RuntimeDataProvider m_RuntimeDataProvider;
        private IEnemyViewFactory m_EnemyFactory;
        private IHeroViewFactory m_HeroFactory;
        
        // TO REMOVE
        [SerializeField] UINotification uiNotification;
        
        [SerializeField] Transform[] m_EnemiesSpawnPoints;
        [SerializeField] Transform[] m_HeroesSpawnPoints;
        
        private IReadOnlyList<CMSEntityPfb> m_EnemiesInBattle; 
        private IReadOnlyList<SaveHeroState> m_HeroesInBattle;
        
        private BattleStage m_CurrentBattleStage;
        private int m_EnemyFightedCounter = 0;
        
        
        public void StartBattle(){
            
            ConfigureBattleData();
            
            SetupBattleStage();

            NotifyBattleStageReady();
        }
        
        private void NotifyBattleStageReady()
            => m_SignalBus.SendSignal(new BattleStageReadySignal(m_CurrentBattleStage));
        
        private void SetupBattleStage(){
            
            List<HeroView> HeroesInBattle = new();
            List<EnemyView> EnemiesInBattle = new();
            
            int heroIndex = 0;
            foreach(var model in m_HeroesInBattle){
                var hero = m_HeroFactory.CreateFromSaveHeroState(model, m_HeroesSpawnPoints[heroIndex++]);
                HeroesInBattle.Add(hero);
                m_SignalBus.SendSignal(new HeroSpawnedSignal(hero));
            }
            
            int limit = m_EnemiesSpawnPoints.Length;
            for (int i = m_EnemyFightedCounter; i < m_EnemiesInBattle.Count; i++){
                var enemy = m_EnemyFactory.CreateFromCMS(m_EnemiesInBattle[i], m_EnemiesSpawnPoints[--limit]);
                EnemiesInBattle.Add(enemy);
                m_SignalBus.SendSignal(new EnemySpawnedSignal(enemy));
                if(limit == 0){break;}
            }

            m_CurrentBattleStage = new BattleStage(EnemiesInBattle, HeroesInBattle);
        }      
               
        private void ConfigureBattleData(){
            var location = m_RuntimeDataProvider.m_CurrentLocationModel;
            var dungeon = location.GetTag<TagDungeon>();
            
            if(dungeon == null){throw new System.Exception("Unable to setup battle, because current location have not TagDungeon.");}

            m_EnemiesInBattle = dungeon.GetEnemies();
            m_HeroesInBattle = m_RuntimeDataProvider.m_PlayerState.GetHeroes();
        }
        
        private void OnEnemyDied(EnemyDiedSignal signal){
            if(!m_CurrentBattleStage.GetEnemies().Contains(signal.GetEnemy())){return ;}

            m_EnemyFightedCounter++;
            m_CurrentBattleStage.EnemyKilled();
            
            Destroy(signal.GetEnemy().gameObject);
            
            if(m_CurrentBattleStage.isCompleted()){
                StartCoroutine(Won());
            }
        }
        
        private void OnHeroDied(HeroDiedSignal signal){
            m_CurrentBattleStage.HeroDied();
            
            if(m_CurrentBattleStage.isAllHeroesDied()){
                StartCoroutine(Lost());
            }
        }
        
        
        private IEnumerator Won(){
            
            if(m_EnemyFightedCounter < m_EnemiesInBattle.Count){
                StartBattle();
                yield break;
            }
            
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
