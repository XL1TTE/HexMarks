
using Project.EventBus;
using Project.EventBus.Signals;
using UnityEngine;
using Zenject;

namespace Project.DataResolving.DataRequestResolvers{
    public class EnemyTargetResolver : IDataRequestResolver
    {
        private GameObject m_CurrentEnemyTarget;
        private void SetCurrentEnemyTarget(SetEnemyTargetSignal signal) => m_CurrentEnemyTarget = signal.GetTarget();
        
        
        [Inject]
        private void Construct(SignalBus signalBus){
            signalBus.Subscribe<SetEnemyTargetSignal>(SetCurrentEnemyTarget);
        }
        
        public bool CanResolve(DataRequierment req)
        {
            return req.GetReqName() == "EnemyTarget" && req.GetReqDataType() == typeof(GameObject);
        }

        public object Resolve(DataRequierment req)
        {
            return m_CurrentEnemyTarget;
        }
    }
}
