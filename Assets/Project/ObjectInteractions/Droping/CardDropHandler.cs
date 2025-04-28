
using Project.Cards;
using Project.Layouts;
using UnityEngine;

namespace Project.ObjectInteractions{
    public class CardDropHandler : MonoBehaviour, IDropHandler
    {
        [SerializeField] LimitCardsLayout m_cardLayout;
        public void HandleDrop(GameObject obj)
        {
            if(!obj.TryGetComponent<CardView>(out var cardView)){return;}
                          
            if(!m_cardLayout.TryClaim(cardView)){return;};
            
            cardView.UseCard();
        }
    }
}
