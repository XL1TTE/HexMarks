using System.Collections.Generic;
using Project.Actors;
using Project.EventBus;
using Project.EventBus.Signals;
using Zenject;

namespace Project.DataResolving.DataRequestResolvers{
    public class HeroesInBattleReqResolver : IDataRequestResolver
    {
        private IReadOnlyList<HeroView> m_CurrentHeroesInButtle;
        private void OnBattleStartInteraction(BattleStageReadySignal signal)
        {
            m_CurrentHeroesInButtle = signal.Stage.GetHeroes();
        }

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<BattleStageReadySignal>(OnBattleStartInteraction);
        }
        public bool CanResolve(DataRequierment req)
        {
            return req.GetReqName() == "HeroesInBattle" && req.GetReqDataType() == typeof(List<HeroView>);
        }

        public object Resolve(DataRequierment req)
        {
            return m_CurrentHeroesInButtle;
        }
    }
}
