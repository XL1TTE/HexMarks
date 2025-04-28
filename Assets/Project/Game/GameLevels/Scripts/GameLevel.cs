using System.Collections.Generic;
using UnityEngine;

namespace Project.Game.GameLevels{
    
    
    [CreateAssetMenu(fileName = "GameLevel", menuName = "Game/GameLevel")]
    public class GameLevel : ScriptableObject{
        [SerializeField] private List<GameObject> m_Enemies = new();
        public IReadOnlyList<GameObject> GetEnemies() => m_Enemies;
        
        
    }
}
