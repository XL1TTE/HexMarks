using Project.Cards;
using Project.Enemies;

namespace Project.EventBus.Signals{
    public class CardUsedOnEnemySignal: ISignal{
        
        public CardUsedOnEnemySignal(EnemyView target, CardView cardView){
            m_Target = target;
            m_CardView = cardView;
        }
        private EnemyView m_Target;
        public EnemyView GetTarget() => m_Target;
        private CardView m_CardView;
        public CardView GetCardView() => m_CardView;
        
    }
}
