using System;
using System.Collections.Generic;
using CMSystem;
using Project.Cards;
using Project.Data.CMS.Tags.Generic;
using Project.DataResolving;
using Project.EventBus;
using UnityEngine;
using Zenject;

namespace Project.Factories{
    public class CardFactory : ICardFactory
    {
        [Inject]
        public CardFactory(CardViewObjectPool a_cardViewPool, SignalBus signalBus)
        {
            m_cardViewPool = a_cardViewPool;
            m_signalBus = signalBus;
        }
        
        private readonly CardViewObjectPool m_cardViewPool;
        private readonly SignalBus m_signalBus;
        
        public Card CreateCardFromModel(CMSEntity model, bool isActive = true)
        {
            CardView view = m_cardViewPool.Get(model.id, isActive);

            CardState state = new CardState(model);

            Card card = new Card(state, view, m_signalBus);

            ConfigureCardView(card, view);

            return card;
        }

        private void ConfigureCardView(Card card, CardView cardView){
            cardView.Init(card);
        }
            
        
        public Card CreateNewCard()
        {            
            throw new NotImplementedException();
        }
    }
    
}

