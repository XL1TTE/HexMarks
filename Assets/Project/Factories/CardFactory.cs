using System.Collections;
using Project.Cards;
using Zenject;

namespace Project.Factories{
    
    
    public interface ICardFactory{
        Card CreateNewCard();
        Card CreateCardFromDef(CardDefenition def);
    }
    public class CardFactory : ICardFactory
    {
        [Inject]
        public CardFactory(CardViewObjectPool a_cardViewPool){
            m_cardViewPool = a_cardViewPool;
        }
        
        private readonly CardViewObjectPool m_cardViewPool;
        
        public Card CreateCardFromDef(CardDefenition def)
        {
            throw new System.NotImplementedException();
        }

        public Card CreateNewCard()
        {
            CardModel model = new CardModel();
            
            CardView view = m_cardViewPool.Get();
            
            Card controller = new Card(model, view);
            
            return controller;
        }
    }
}

