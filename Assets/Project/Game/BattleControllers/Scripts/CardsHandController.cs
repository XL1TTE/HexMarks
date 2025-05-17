using Project.Actors;
using Project.Cards;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.Layouts;
using UnityEngine;
using Zenject;

namespace Project.Game.Battle.Controllers
{
    public class CardsHandController : MonoBehaviour
    {
        [Inject]
        private void Construct(SignalBus signalBus, ICardFactory cardFactory)
        {
            m_SignalBus = signalBus;
            m_CardFactory = cardFactory;
        }

        void OnEnable()
        {
            m_SignalBus.Subscribe<RequestDrawCardsSignal>(DrawCardsRequestProccess);
            m_SignalBus.Subscribe<RequestCardsDraggingStateSwitchSignal>(CardsDragSwitchRequestProccess);
        }
        void OnDisable()
        {
            m_SignalBus.Unsubscribe<RequestDrawCardsSignal>(DrawCardsRequestProccess);
        }

        private SignalBus m_SignalBus;
        private ICardFactory m_CardFactory;
        
        [SerializeField] private CardHand m_CardsHand;
        
        private void DrawCardsRequestProccess(RequestDrawCardsSignal signal){
            if(signal.ClearHand){ClearHand();}
            DrawCards(signal.Hero, signal.Amount);
        }


        private void CardsDragSwitchRequestProccess(RequestCardsDraggingStateSwitchSignal signal)
        {
            if(signal.isEnabled) {TurnOnCardsDragging();}
            else{TurnOffCardsDragging();}
        }

        private void TurnOnCardsDragging(){
            foreach (var card in m_CardsHand.GetAllItems())
            {
                card.EnableDragging();
            }
        }
        private void TurnOffCardsDragging(){
            foreach (var card in m_CardsHand.GetAllItems())
            {
                card.DisableDragging();
            }
        }

        private void ClearHand() =>
            m_CardsHand.ClearHand();

        private void DrawCards(HeroView hero, int amount)
        {
            var cards = hero.GetState().m_deck.GetCards();
            
            for (int i = 0; i < amount; i++)
            {
                var cardPick = cards[Random.Range(0, cards.Count)];
                
                Card card = m_CardFactory.CreateCardFromModel(cardPick);

                m_CardsHand.TryClaim(card.GetView());
            }
        }

    }
}
