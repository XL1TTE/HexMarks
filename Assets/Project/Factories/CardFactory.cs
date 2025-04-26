using System.Collections;
using System.Collections.Generic;
using Project.Cards;
using UnityEngine;
using Zenject;

namespace Project.Factories{
    
    
    public interface ICardFactory{
        Card CreateNewCard();
        Card CreateCardFromDef(CardDefenition def);
    }
    public class CardFactory : ICardFactory
    {
        public CardFactory(DiContainer a_container, CardView a_prefab){
            m_container = a_container;
            m_prefab = a_prefab;
        }
        
        private readonly DiContainer m_container;
        private readonly CardView m_prefab;
        
        public Card CreateCardFromDef(CardDefenition def)
        {
            throw new System.NotImplementedException();
        }

        public Card CreateNewCard()
        {
            CardModel model = new CardModel();
            
            CardView view = m_container.InstantiatePrefabForComponent<CardView>(m_prefab);
            
            CardController controller = new CardController(model);
            
            view.Init(controller);
            if(view.TryGetComponent<CardInteraction>(out var cardInteraction)){
                cardInteraction.Init(controller);
            }
            
            return new Card(view, model, controller);
        }
    }
}

