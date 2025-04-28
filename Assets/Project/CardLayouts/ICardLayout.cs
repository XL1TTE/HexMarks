using Project.Cards;

namespace Project.Layouts
{
    public interface ICardLayout{
        bool TryClaim(CardView a_card);
        void Release(CardView a_card);
        void ClearHand();
    }
}
