using System.Collections.Generic;
using Project.Factories;
using Project.Game.GameLevels;
using Project.Utilities.Extantions;
using UnityEngine;
using Zenject;

namespace Project.Game.Battle{
    
    public class BattleController : MonoBehaviour
    {
        [Inject]
        private void Construct(IEnemyViewFactory enemyViewFactory)
        {
            m_enemyViewFactory = enemyViewFactory;
        }
        [SerializeField] private List<GameLevel> m_Levels;
        [SerializeField] private List<Transform> m_EnemySpawnPoints;
        private IEnemyViewFactory m_enemyViewFactory;

        void Start()
        {
            NextLevel();
        }

        private void NextLevel(){
            var level = m_Levels.Dequeue();
            var enemies = level.GetEnemies();
            
            int index = 0;
            foreach (var p in m_EnemySpawnPoints){
                m_enemyViewFactory.CreateEnemy(enemies[index], p);
                if(++index >= enemies.Count){return;}
            }
        }
    }
    
}
