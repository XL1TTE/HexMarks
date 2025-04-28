using System.Collections.Generic;
using Project.Game.GameLevels;
using Project.Utilities.Extantions;
using UnityEngine;

namespace Project.Game.Battle{
    
    public class BattleController : MonoBehaviour{
        [SerializeField] private List<GameLevel> m_Levels;
        [SerializeField] private List<Transform> m_EnemySpawnPoints;

        void Start()
        {
            NextLevel();
        }

        private void NextLevel(){
            var level = m_Levels.Dequeue();
            var enemies = level.GetEnemies();
            
            int index = 0;
            foreach (var p in m_EnemySpawnPoints){
                Object.Instantiate(enemies[index], p);
                if(++index >= enemies.Count){return;}
            }
        }
    }
    
}
