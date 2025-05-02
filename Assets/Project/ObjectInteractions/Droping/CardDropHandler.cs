
using Project.Cards;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Layouts;
using UnityEngine;
using Zenject;

namespace Project.ObjectInteractions{
    
    public class CardDropHandler : MonoBehaviour, IDropHandler
    {
        [Inject]
        private void Construct(SignalBus signalBus){
            m_signalBus = signalBus;
        }
        private SignalBus m_signalBus;

        [SerializeField] LimitCardsLayout m_cardLayout;
        [SerializeField] DropHandlerType HandlerType;
                
        private void OnCardDropOnEnemy(CardView cardView){
            Enemy enemy;
            if(!TryGetComponent(out enemy)){
                enemy = GetComponentInParent<Enemy>();
            }
            
            m_signalBus.SendSignal(new SetEnemyTargetSignal(enemy));

            m_signalBus.SendSignal(new CardUsedOnEnemySignal(enemy, cardView));
        }

        public void HandleDrop(GameObject obj)
        {
            if(!obj.TryGetComponent<CardView>(out var cardView)){return;}
                          
            if(!m_cardLayout.TryClaim(cardView)){return;};
            
            if(HandlerType == DropHandlerType.Enemy){
                OnCardDropOnEnemy(cardView);
            }
        }
    }
}
