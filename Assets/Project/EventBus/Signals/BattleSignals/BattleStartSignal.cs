using System.Collections.Generic;
using Project.Enemies;
using Project.Player;

namespace Project.EventBus.Signals{
    public class BattleStartSignal: ISignal{
        
        private List<EnemyView> m_EnemiesInBattle;
        public List<EnemyView> GetEnemiesInBattle() => m_EnemiesInBattle;
        
        private PlayerData m_PlayerData;
        public PlayerData GetPlayerInBattle() => m_PlayerData;
        
        public BattleStartSignal(
            List<EnemyView> enemiesInBattle,
            PlayerData playerInBattle
        )
        {
            m_EnemiesInBattle = enemiesInBattle;
            m_PlayerData = playerInBattle;
        }
    }
    
}
