using CMSystem;
using Project.Cards;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using XL1TTE.GameActions;
using Zenject;

namespace Project.DataResolving.DataRequestResolvers{
    public class LastAllyCardPlayedReqResolver : IDataRequestResolver
    {
        [Inject]
        private void Construct(SignalBus signalBus, ICardFactory cardFactory){
            m_cardFactory = cardFactory;


            signalBus.Subscribe<HeroTurnSignal>(OnHeroTurn);
            signalBus.Subscribe<CardPlayedSignal>(OnCardUsed);
        }
        
        private ICardFactory m_cardFactory;
        
        private string m_lastCardPlayed_Id;
        private string m_lastCardPlayedByAlly_Id;

        private string m_currentTurnHero_Id;
        private string m_previousTurnHero_Id;

        private void OnCardUsed(CardPlayedSignal signal)
        {
            m_lastCardPlayed_Id = "" + signal.card.m_state.model.id;
        }

        private void OnHeroTurn(HeroTurnSignal signal)
        {
            m_previousTurnHero_Id = m_currentTurnHero_Id;
            m_currentTurnHero_Id = signal.hero.GetID();
            
            if(m_currentTurnHero_Id != m_previousTurnHero_Id){
                m_lastCardPlayedByAlly_Id = m_lastCardPlayed_Id;
            }
        }

        public bool CanResolve(DataRequest req)
        {
            return req.Key == "LastAllyCardPlayed" && req.Type == typeof(Card);
        }

        public object Resolve(DataRequest req)
        {
            if(m_lastCardPlayedByAlly_Id != null){
                return m_cardFactory.CreateCardFromModel(CMS.Get<CMSEntity>(m_lastCardPlayedByAlly_Id), false);
            }
            return null;
        }
    }
}
