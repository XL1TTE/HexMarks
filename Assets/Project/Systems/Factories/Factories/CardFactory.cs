using System;
using System.Collections.Generic;
using CMSystem;
using Project.Cards;
using Project.DataResolving;
using Project.EventBus;
using UnityEngine;
using Zenject;

namespace Project.Factories{
    public class CardFactory : ICardFactory
    {
        [Inject]
        public CardFactory(CardViewObjectPool a_cardViewPool)
        {
            m_cardViewPool = a_cardViewPool;
        }
        
        private readonly CardViewObjectPool m_cardViewPool;
        
        public Card CreateCardFromModel(CMSEntity model, bool isActive = true)
        {
            CardView view = m_cardViewPool.Get(model.id, isActive);

            CardState state = new CardState(model);

            Card controller = new Card(state, view);
            
            return controller;
        }

        public Card CreateNewCard()
        {            
            throw new NotImplementedException();
        }
    }
    
}

