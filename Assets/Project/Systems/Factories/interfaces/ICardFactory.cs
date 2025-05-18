using CMSystem;
using Project.Cards;

namespace Project.Factories{
    public interface ICardFactory{
        Card CreateNewCard();
        Card CreateCardFromModel(CMSEntity model, bool isActive = true);
    }
}

