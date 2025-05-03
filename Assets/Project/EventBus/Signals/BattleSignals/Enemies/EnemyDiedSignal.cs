using Project.Enemies;

namespace Project.EventBus.Signals{
    public class EnemyDiedSignal: ISignal{
        public EnemyDiedSignal(EnemyView enemy)
        {
            m_Enemy = enemy;
        }
        private EnemyView m_Enemy;
        public EnemyView GetEnemy() => m_Enemy;

    }

}
