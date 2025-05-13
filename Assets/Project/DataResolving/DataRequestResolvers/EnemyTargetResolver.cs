using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
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
        
        public bool CanResolve(DataRequierment req)
        {
            return req.GetReqName() == "EnemyTarget" && req.GetReqDataType() == typeof(EnemyView);
        }

        public object Resolve(DataRequierment req)
        {
            return m_CurrentEnemyTarget;
        }
    }
}
