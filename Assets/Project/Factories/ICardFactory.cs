using Project.Cards;

namespace Project.Factories{
    public interface ICardFactory{
        Card CreateNewCard();
        Card CreateCardFromDef(CardDefenition def);
    }
}

