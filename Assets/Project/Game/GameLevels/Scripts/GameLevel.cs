using System.Collections.Generic;
using Project.Enemies;
using UnityEngine;

namespace Project.Game.GameLevels{
    
    
    [CreateAssetMenu(fileName = "GameLevel", menuName = "Game/GameLevel")]
    public class GameLevel : ScriptableObject{
        [SerializeField] private List<EnemyDefenition> m_Enemies = new();
        public IReadOnlyList<EnemyDefenition> GetEnemies() => m_Enemies;
        
        
    }
}
