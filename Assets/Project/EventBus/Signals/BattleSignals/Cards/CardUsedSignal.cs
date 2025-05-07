using Project.Cards;
using Project.Enemies;

namespace Project.EventBus.Signals{
    public class CardUsedSignal: ISignal{
        
        public CardUsedSignal(CardView cardView){
            m_CardView = cardView;
        }
        private CardView m_CardView;
        public CardView GetCardView() => m_CardView;
        
    }
}
