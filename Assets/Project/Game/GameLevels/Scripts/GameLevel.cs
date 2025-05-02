using System.Collections.Generic;
using Project.Enemies;
using UnityEngine;

namespace Project.Game.GameLevels{
    
    
    [CreateAssetMenu(fileName = "GameLevel", menuName = "Game/GameLevel")]
    public class GameLevel : ScriptableObject{
        [SerializeField] private List<Enemy> m_Enemies = new();
        public IReadOnlyList<Enemy> GetEnemies() => m_Enemies;
        
        
    }
}
