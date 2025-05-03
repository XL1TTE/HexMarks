using Project.Enemies;

namespace Project.EventBus.Signals{
    public class EnemyHealthChangedSignal : ISignal{
        public EnemyHealthChangedSignal(EnemyView enemy){
            m_Enemy = enemy;
        }
        private EnemyView m_Enemy;
        public EnemyView GetEnemy() => m_Enemy;

    }
    
}
