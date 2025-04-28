
using Project.Cards;
using UnityEngine;

namespace Project.Layouts{
    public class LimitCardsLayout : CardHand
    {
        [SerializeField, Range(1, 10)] int m_MaxItems = 1;
        public override bool TryClaim(CardView a_card)
        {
            if(m_ClaimedItems.Count >= m_MaxItems) {return false;}
            
            return base.TryClaim(a_card);
        }
        
    }
}
