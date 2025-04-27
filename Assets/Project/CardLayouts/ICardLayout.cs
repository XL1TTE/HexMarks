using Project.Cards;

namespace Project.Layouts
{
    public interface ICardLayout{
        void Claim(Card a_card);
        void Release(Card a_card);
        void ClearHand();
    }
}
