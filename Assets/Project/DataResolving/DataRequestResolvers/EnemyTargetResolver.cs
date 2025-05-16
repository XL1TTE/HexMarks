using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using XL1TTE.GameActions;
using Zenject;

namespace Project.DataResolving.DataRequestResolvers{
    public class EnemyTargetResolver : IDataRequestResolver
    {
        private EnemyView m_CurrentEnemyTarget;
        private void SetCurrentEnemyTarget(SetEnemyTargetSignal signal) {
            m_CurrentEnemyTarget = signal.GetTarget();
        }
        
        [Inject]
        private void Construct(SignalBus signalBus){
            signalBus.Subscribe<SetEnemyTargetSignal>(SetCurrentEnemyTarget);
        }
        
        public bool CanResolve(DataRequest req)
        {
            return req.Key == "EnemyTarget" && req.Type == typeof(EnemyView);
        }

        public object Resolve(DataRequest req)
        {
            return m_CurrentEnemyTarget;
        }
    }
}
