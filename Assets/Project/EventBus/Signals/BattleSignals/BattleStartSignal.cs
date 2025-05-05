using System.Collections.Generic;
using Project.Enemies;
using Project.Player;

namespace Project.EventBus.Signals{
    public class BattleStartSignal: ISignal{
        
        private PlayerInBattle m_PlayerInBattle;
        public PlayerInBattle GetPlayerInBattle() => m_PlayerInBattle;
        private List<EnemyView> m_EnemiesInBattle;
        public List<EnemyView> GetEnemiesInBattle() => m_EnemiesInBattle;
        
        public BattleStartSignal(
            List<EnemyView> enemiesInBattle, PlayerInBattle playerInBattle)
        {
            m_EnemiesInBattle = enemiesInBattle;
            m_PlayerInBattle = playerInBattle;
        }
    }
}
