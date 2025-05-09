using System.Collections.Generic;
using CMSystem;
using Project.Enemies;
using UnityEngine;

namespace Project.Game.GameLevels{
    
    
    [CreateAssetMenu(fileName = "GameLevel", menuName = "Game/GameLevel")]
    public class GameLevel : ScriptableObject{
        [SerializeField] private List<CMSEntityPfb> m_Enemies = new();
        public IReadOnlyList<CMSEntityPfb> GetEnemies() => m_Enemies;
        
        
    }
}
