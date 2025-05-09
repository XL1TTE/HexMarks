using System.Collections.Generic;
using Project.Cards;
using Project.DataResolving;
using UnityEngine;
using Zenject;

namespace Project.Factories{
    public class CardFactory : ICardFactory
    {
        [Inject]
        public CardFactory(CardViewObjectPool a_cardViewPool, DataResolver dataRosolver, List<CardModel> cardModels)
        {
            m_cardViewPool = a_cardViewPool;
            m_dataResolver = dataRosolver;
            m_CardModels = cardModels;

        }
        
        private readonly CardViewObjectPool m_cardViewPool;
        private readonly DataResolver m_dataResolver;
        private readonly List<CardModel> m_CardModels;
        
        public Card CreateCardFromDef(CardDefenition def)
        {
            throw new System.NotImplementedException();
        }

        public Card CreateNewCard()
        {
            CardModel model = m_CardModels[Random.Range(0, m_CardModels.Count)];
            
            CardView view = m_cardViewPool.Get();
            
            view.GetRenderer().sprite = model.GetCardSprite();
            
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

