using System.Collections;
using System.Collections.Generic;
using Project.Actors;
using Project.Enemies;

namespace Project.Game.Battle{
    public class BattleStage{
        
        public BattleStage(List<EnemyView> enemies, List<HeroView> heroes){
            m_Enemies = enemies;
            m_Heroes = heroes;
        }
        
        private List<EnemyView> m_Enemies;
        private List<HeroView> m_Heroes;
        private int m_EnemiesKilled = 0;
        private int m_HeroesDied = 0;
        public void EnemyKilled() => m_EnemiesKilled++;
        public void HeroDied() => m_HeroesDied++;
        public bool isCompleted() => m_Enemies.Count == m_EnemiesKilled;
        public bool isAllHeroesDied() => m_Heroes.Count == m_HeroesDied;
        
        public IReadOnlyList<EnemyView> GetEnemies() => m_Enemies;
        public IReadOnlyList<HeroView> GetHeroes() => m_Heroes;
    }
}
