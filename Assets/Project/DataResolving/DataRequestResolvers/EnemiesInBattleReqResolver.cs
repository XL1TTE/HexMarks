using System.Collections.Generic;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using UnityEngine;
using Zenject;

namespace Project.DataResolving.DataRequestResolvers{
    public class EnemiesInBattleReqResolver : IDataRequestResolver
    {
        private IReadOnlyList<EnemyView> m_CurrentEnemiesInBattle;
        private void OnBattleStageReady(BattleStageReadySignal signal) {
            m_CurrentEnemiesInBattle = signal.Stage.GetEnemies();
        }

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<BattleStageReadySignal>(OnBattleStageReady);
        }

        public bool CanResolve(DataRequierment req)
        {
            return req.GetReqName() == "EnemiesInBattle" && req.GetReqDataType() == typeof(List<EnemyView>);
        }

        public object Resolve(DataRequierment req)
        {
            Debug.Log(m_CurrentEnemiesInBattle.Count);
            return m_CurrentEnemiesInBattle;
        }
    }
}
