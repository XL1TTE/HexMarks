using Project.Enemies;

namespace Project.EventBus.Signals{
    public class EnemyTurnSignal: ISignal{
        private EnemyView m_enemy;
        public EnemyView GetEnemy() => m_enemy;
        public EnemyTurnSignal(EnemyView enemy)
        {
            m_enemy = enemy;
        }
    }
    
}
