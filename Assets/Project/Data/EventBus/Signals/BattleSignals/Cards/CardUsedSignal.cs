using Project.Cards;
using Project.Enemies;
using Project.JobSystem;

namespace Project.EventBus.Signals{
    public class CardUsedSignal: ISignal{
        
        public CardUsedSignal(CardView cardView, JobSequence cardUseSequence){
            m_CardView = cardView;
            m_CardUseSequence = cardUseSequence;
        }
        private CardView m_CardView;
        private JobSequence m_CardUseSequence;
        public CardView GetCardView() => m_CardView;
        public JobSequence GetCardUseSequence() => m_CardUseSequence;        
    }
}
