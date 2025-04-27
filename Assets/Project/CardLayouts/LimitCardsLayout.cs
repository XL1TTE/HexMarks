
using Project.Cards;
using UnityEngine;

namespace Project.Layouts{
    public class LimitCardsLayout : CardHand
    {
        [SerializeField, Range(1, 10)] int m_MaxItems = 1;
        public override void Claim(Card a_card)
        {
            if(m_ClaimedItems.Count >= m_MaxItems) {return;}
            
            base.Claim(a_card);
        }
    }
}
