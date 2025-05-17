using System.Collections.Generic;
using Project.Actors;
using Project.Enemies;
using Project.Game.Battle;

namespace Project.EventBus.Signals{
    public class BattleStageReadySignal: ISignal{
        
        public BattleStage Stage;
        
        public BattleStageReadySignal(BattleStage stage)
        {
            Stage = stage;
        }
    }
    
    public class NextEnemyWaveSignal: ISignal{
        
        private List<EnemyView> m_Enemies;
        public List<EnemyView> GetEnemies() => m_Enemies;
        
        public NextEnemyWaveSignal(
            List<EnemyView> enemies)
        {
            m_Enemies = enemies;

        }
    }
}
