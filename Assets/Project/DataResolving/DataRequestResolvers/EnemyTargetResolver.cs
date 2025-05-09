using System.Collections;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using UnityEngine;
using Zenject;

namespace Project.DataResolving.DataRequestResolvers{
    public class EnemyTargetResolver : IDataRequestResolver
    {
        private EnemyView m_CurrentEnemyTarget;
        private IEnumerator SetCurrentEnemyTarget(SetEnemyTargetSignal signal) {
            m_CurrentEnemyTarget = signal.GetTarget();
            yield return null;
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
