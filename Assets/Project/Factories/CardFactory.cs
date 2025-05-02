using Project.Cards;
using Project.DataResolving;
using Zenject;

namespace Project.Factories{
    public class CardFactory : ICardFactory
    {
        [Inject]
        public CardFactory(CardViewObjectPool a_cardViewPool, DataRosolver dataRosolver){
            m_cardViewPool = a_cardViewPool;
            m_dataResolver = dataRosolver;
        }
        
        private readonly CardViewObjectPool m_cardViewPool;
        private readonly DataRosolver m_dataResolver;
        
        public Card CreateCardFromDef(CardDefenition def)
        {
            throw new System.NotImplementedException();
        }

        public Card CreateNewCard()
        {
            CardModel model = new CardModel();
            
            CardView view = m_cardViewPool.Get();
            
            Card controller = new Card(model, view, m_dataResolver);
            
            return controller;
        }
    }
    
    public class CardModelFactory{
        
        [Inject]
        private void Construct(){
            
        }    
    }
}

