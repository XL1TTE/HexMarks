using System.Collections;
using System.Collections.Generic;
using CMSystem;
using Project.Actors;
using Project.Data.CMS.Tags;
using Project.Data.SaveFile;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.UI;
using Project.Utilities.Extantions;
using UnityEngine;
using Zenject;

namespace Project.Game.Battle{
    
    public class BattleController : MonoBehaviour
    {
        [Inject]
        private void Construct(
            SignalBus signalBus, 
            IEnemyViewFactory enemyViewFactory,
            IHeroViewFactory heroViewFactory,
            RuntimeDataProvider runtimeDataProvider)
        {
            m_enemyViewFactory = enemyViewFactory;
            m_heroViewFactory = heroViewFactory;
            m_SignalBus = signalBus;
            m_RuntimeDataProvider = runtimeDataProvider;
        }

        [SerializeField] private List<Transform> m_EnemySpawnPoints;
        [SerializeField] private List<Transform> m_HeroSpawnPoints;
        
        [SerializeField] private GameObject WinMessage;
        
        private RuntimeDataProvider m_RuntimeDataProvider;
        private IEnemyViewFactory m_enemyViewFactory;
        private IHeroViewFactory m_heroViewFactory;
        private SignalBus m_SignalBus;
        
        
        private Queue<CMSEntityPfb> m_EnemiesToFight = new();
        private List<HeroView> m_HeroesInBattle = new();

        void Awake()
        {
            WinMessage.SetActive(false);
        }

        public void Initialize(){
            m_SignalBus.Subscribe<PlayerWonBattleSignal>(OnPlayerWon);
            m_SignalBus.Subscribe<PlayerLostBattleSignal>(OnPlayerLost);
        }

        public void StartBattle(){
            
            if(m_RuntimeDataProvider.m_CurrentLocationModel != null){
                m_RuntimeDataProvider.m_CurrentLocationModel.Is<TagDungeon>(out var tagDungeon);
                m_EnemiesToFight = new Queue<CMSEntityPfb>(tagDungeon.GetEnemies());

                int index = 0;
                foreach (var h in m_RuntimeDataProvider.m_PlayerState.m_Heroes)
                {

                    var hero = m_heroViewFactory.CreateFromSaveHeroState(h, m_HeroSpawnPoints[index++]);
                    m_SignalBus.SendSignal(new HeroSpawnedSignal(hero));

                    m_HeroesInBattle.Add(hero);
                }
            }

            NextLevel();
        }

        private void NextLevel(){

            List<EnemyView> m_EnemiesInBattle = new();
                        
            foreach (var p in m_EnemySpawnPoints){
                if (m_EnemiesToFight.Count == 0) { return; }

                var enemy = m_enemyViewFactory.CreateFromCMS(m_EnemiesToFight.Dequeue(), p);
                m_EnemiesInBattle.Add(enemy);

                m_SignalBus.SendSignal(new EnemySpawnedSignal(enemy));
            }
            m_SignalBus.SendSignal(new BattleStartSignal(m_EnemiesInBattle, m_HeroesInBattle));
        }

        private IEnumerator OnPlayerWon(PlayerWonBattleSignal signal)
        {
            yield return OnLevelComplete();
        }
        private IEnumerator OnPlayerLost(PlayerLostBattleSignal signal){
            yield return OnPlayerLostRoutine();
        }

        private IEnumerator OnLevelComplete(){
            
            yield return UINotification.current.ShowNotification("You won!", 2.0f, "#46EE59".ToColor());

            NextLevel();
        }
        
        private IEnumerator OnPlayerLostRoutine(){
            yield return UINotification.current.ShowNotification("You lost!", 2.0f, "#751B13".ToColor());
        }
    }

}
