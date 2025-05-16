using System.Collections.Generic;
using Project.Actors;
using Project.EventBus;
using Project.EventBus.Signals;
using XL1TTE.GameActions;
using Zenject;

namespace Project.DataResolving.DataRequestResolvers{
    public class HeroesInBattleReqResolver : IDataRequestResolver
    {
        private IReadOnlyList<HeroView> m_CurrentHeroesInButtle;
        private void OnBattleStageReady(BattleStageReadySignal signal)
        {
            m_CurrentHeroesInButtle = signal.Stage.GetHeroes();
        }

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<BattleStageReadySignal>(OnBattleStageReady);
        }

        public bool CanResolve(DataRequest req)
        {
            return req.Key == "HeroesInBattle" && req.Type == typeof(List<HeroView>);
        }

        public object Resolve(DataRequest req)
        {
            return m_CurrentHeroesInButtle;
        }
    }
}
