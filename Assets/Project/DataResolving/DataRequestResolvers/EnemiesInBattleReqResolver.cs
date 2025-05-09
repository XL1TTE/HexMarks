
using System.Collections;
using System.Collections.Generic;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Zenject;

namespace Project.DataResolving.DataRequestResolvers{
    public class EnemiesInBattleReqResolver : IDataRequestResolver
    {
        private List<EnemyView> m_CurrentEnemiesInButtle;
        private IEnumerator OnBattleStartInteraction(BattleStartSignal signal) {
            m_CurrentEnemiesInButtle = signal.GetEnemiesInBattle();
            yield return null;
        }

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<BattleStartSignal>(OnBattleStartInteraction);
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
