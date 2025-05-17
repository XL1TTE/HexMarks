using System.Collections;
using Project.DataResolving;
using Project.JobSystem;

namespace Project.Cards{
    public class Card
    {
        public Card(CardState state, CardView cardView){
            m_state = state;
            m_view = cardView;
            m_view.Init(this);
        }
        
        private CardState m_state;
        private CardView m_view;
        public CardView GetView() => m_view;
        public CardState GetState() => m_state;
    }
}

