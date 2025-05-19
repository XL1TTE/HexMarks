using Project.EventBus;
using Project.EventBus.Signals;
using UnityEngine;

namespace Project.Cards{
    public class Card
    {
        public Card(CardState state, CardView cardView, SignalBus signalBus){
            m_state = state;
            m_view = cardView;
            m_signalBus = signalBus;        
        }
        
        
        #region Services
            private readonly SignalBus m_signalBus;
        #endregion
        
        public CardState m_state {get; private set;}
        public CardView m_view {get; private set;}

        public void PlayCard()
        {
            m_signalBus.SendSignal(new CardPlayedSignal(this));
        }
    }
}

