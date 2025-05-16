using System.Collections.Generic;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using UnityEngine;
using XL1TTE.GameActions;
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

        public bool CanResolve(DataRequest req)
        {
            return req.Key == "EnemiesInBattle" && req.Type == typeof(List<EnemyView>);
        }

        public object Resolve(DataRequest req)
        {
            return m_CurrentEnemiesInBattle;
        }
    }
}
