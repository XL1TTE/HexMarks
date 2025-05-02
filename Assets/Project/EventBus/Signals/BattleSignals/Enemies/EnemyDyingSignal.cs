using Project.Enemies;

namespace Project.EventBus.Signals{
    public class EnemyDyingSignal : ISignal
    {
        public EnemyDyingSignal(Enemy enemy)
        {
            m_Enemy = enemy;
        }
        private Enemy m_Enemy;
        public Enemy GetEnemy() => m_Enemy;

    }

}
