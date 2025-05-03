using System.Collections;
using System.Collections.Generic;
using Project.Cards;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.Game.Battle.UI;
using Project.Game.GameLevels;
using Project.Layouts;
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
            IEnemyViewFactory enemyViewFactory)
        {
            m_enemyViewFactory = enemyViewFactory;
            m_SignalBus = signalBus;
        }

        [SerializeField] private List<GameLevel> m_Levels;
        [SerializeField] private List<Transform> m_EnemySpawnPoints;
        
        [SerializeField] private GameObject WinMessage;
        
        private IEnemyViewFactory m_enemyViewFactory;
        private SignalBus m_SignalBus;
        
        private List<EnemyView> m_EnemiesInBattle = new();

        void Awake()
        {
            WinMessage.SetActive(false);
        }

        public void Initialize(){
            m_SignalBus.Subscribe<EnemyDiedSignal>(OnEnemyDied);
        }
        
        public void StartBattle(){
            NextLevel();
        }

        private void NextLevel(){
            if(m_Levels.Count == 0){return;}
            
            var level = m_Levels.Dequeue();
            var enemies = level.GetEnemies();
            
            int index = 0;
            foreach (var p in m_EnemySpawnPoints){
                
                var enemy = m_enemyViewFactory.CreateFromDef(enemies[index], p);
                m_EnemiesInBattle.Add(enemy);

                m_SignalBus.SendSignal(new EnemySpawnedSignal(enemy));

                if (++index >= enemies.Count){break;}
            }
            m_SignalBus.SendSignal(new BattleStartSignal(m_EnemiesInBattle, Testing.playerData));
        }

        private void OnEnemyDied(EnemyDiedSignal signal){
            m_EnemiesInBattle.Remove(signal.GetEnemy());
            if(m_EnemiesInBattle.Count == 0){
                StartCoroutine(OnLevelComplete());
            }
        }
        
        private IEnumerator OnLevelComplete(){
            WinMessage.SetActive(true);
            yield return new WaitForSeconds(2);

            WinMessage.SetActive(false);
            
            NextLevel();
        }
    }

}
