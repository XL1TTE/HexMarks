
using System.Collections.Generic;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using UnityEngine;
using Zenject;

namespace Project.DataResolving.DataRequestResolvers{
    public class EnemyTargetResolver : IDataRequestResolver
    {
        private EnemyView m_CurrentEnemyTarget;
        private void SetCurrentEnemyTarget(SetEnemyTargetSignal signal) => m_CurrentEnemyTarget = signal.GetTarget();
        
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

    public class EnemiesInBattleReqResolver : IDataRequestResolver
    {
        private List<EnemyView> m_CurrentEnemiesInButtle;
        private void SetCurrentEnemiesInBattle(BattleStartSignal signal) => m_CurrentEnemiesInButtle = signal.GetEnemiesInBattle();

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<BattleStartSignal>(SetCurrentEnemiesInBattle);
        }

        public bool CanResolve(DataRequierment req)
        {
            return req.GetReqName() == "EnemiesInBattle" && req.GetReqDataType() == typeof(List<EnemyView>);
        }

        public object Resolve(DataRequierment req)
        {
            return m_CurrentEnemiesInButtle;
        }
    }
}
