
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
            
            var card = cardView.GetCardController();
              
            m_cardLayout.Claim(card);
            // card.Use()?
        }
    }
}
