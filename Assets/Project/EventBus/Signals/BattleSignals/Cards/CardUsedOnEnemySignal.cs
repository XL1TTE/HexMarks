using Project.Cards;
using Project.Enemies;

namespace Project.EventBus.Signals{
    public class CardUsedOnEnemySignal: ISignal{
        
        public CardUsedOnEnemySignal(Enemy target, CardView cardView){
            m_Target = target;
            m_CardView = cardView;
        }
        private Enemy m_Target;
        public Enemy GetTarget() => m_Target;
        private CardView m_CardView;
        public CardView GetCardView() => m_CardView;
        
    }
}
