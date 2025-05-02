using System.Collections;
using System.Collections.Generic;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.Game.Battle.UI;
using Project.Game.GameLevels;
using Project.UI;
using Project.Utilities.Extantions;
using UnityEngine;
using Zenject;

namespace Project.Game.Battle{
    
    public class BattleController : MonoBehaviour
    {
        [Inject]
        private void Construct(SignalBus signalBus, IEnemyViewFactory enemyViewFactory)
        {
            m_enemyViewFactory = enemyViewFactory;
            m_SignalBus = signalBus;
        }
        
        [SerializeField] private BattleUI m_BattleUI;
        [SerializeField] private List<GameLevel> m_Levels;
        [SerializeField] private List<Transform> m_EnemySpawnPoints;
        
        [SerializeField] private GameObject WinMessage;
        
        private IEnemyViewFactory m_enemyViewFactory;
        private SignalBus m_SignalBus;
        
        private List<Enemy> m_EnemiesInBattle = new();

        void Awake()
        {
            WinMessage.SetActive(false);
        }

        void Start()
        {
            m_SignalBus.Subscribe<EnemyDiedSignal>(OnEnemyDied);

            NextLevel();            
        }

        private void NextLevel(){
            if(m_Levels.Count == 0){return;}
            
            var level = m_Levels.Dequeue();
            var enemies = level.GetEnemies();
            
            int index = 0;
            foreach (var p in m_EnemySpawnPoints){
                var enemy = m_enemyViewFactory.CreateEnemy(enemies[index], p);
                m_EnemiesInBattle.Add(enemy);

                m_SignalBus.SendSignal(new EnemySpawnedSignal(enemy));

                if (++index >= enemies.Count){return;}
            }
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

            NextLevel();

            WinMessage.SetActive(false);
            
        }
    }
    
}
