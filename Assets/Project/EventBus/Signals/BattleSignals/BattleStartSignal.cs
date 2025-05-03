using System.Collections.Generic;
using Project.Enemies;
using Project.Player;

namespace Project.EventBus.Signals{
    public class BattleStartSignal: ISignal{
        
        private List<EnemyView> m_EnemiesInBattle;
        public List<EnemyView> GetEnemiesInBattle() => m_EnemiesInBattle;
        
        public BattleStartSignal(
            List<EnemyView> enemiesInBattle)
        {
            m_EnemiesInBattle = enemiesInBattle;
        }
    }
}
