using Project.Enemies;

namespace Project.EventBus.Signals{
    public class EnemyHealthChangedSignal : ISignal{
        public EnemyHealthChangedSignal(Enemy enemy){
            m_Enemy = enemy;
        }
        private Enemy m_Enemy;
        public Enemy GetEnemy() => m_Enemy;

    }
    
}
