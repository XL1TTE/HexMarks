using System.Collections.Generic;
using Project.Actors;
using Project.Enemies;

namespace Project.Game.Battle{
    public class BattleStage{
        
        public BattleStage(ref List<EnemyView> enemies, ref List<HeroView> heroes){
            m_Enemies = enemies;
            m_Heroes = heroes;
        }
        
        private List<EnemyView> m_Enemies;
        private List<HeroView> m_Heroes;

        public bool isCompleted() => m_Enemies.Count == 0;
        public bool isAllHeroesDied() => m_Heroes.Count == 0;
        
        public IReadOnlyList<EnemyView> GetEnemies() => m_Enemies;
        public IReadOnlyList<HeroView> GetHeroes() => m_Heroes;
    }
}
