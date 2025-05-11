using System.Collections.Generic;
using Project.Actors;
using Project.Enemies;

namespace Project.EventBus.Signals{
    public class BattleStartSignal: ISignal{
        
        private List<HeroView> m_HeroesInBattle;
        private List<EnemyView> m_EnemiesInBattle;
        public List<EnemyView> GetEnemiesInBattle() => m_EnemiesInBattle;
        
        public List<HeroView> GetHeroesInBattle() => m_HeroesInBattle;
        
        public BattleStartSignal(
            List<EnemyView> enemiesInBattle,
            List<HeroView> heroesInBattle)
        {
            m_EnemiesInBattle = enemiesInBattle;
            m_HeroesInBattle = heroesInBattle;
        }
    }
}
