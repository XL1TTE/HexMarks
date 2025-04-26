using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Cards{
    public class CardController
    {
        public CardController(CardModel a_model){
            m_model = a_model;
        }
        private CardModel m_model;

        public void PointerEnterHandler(BaseEventData eventData)
        {
            
        }
        public void PointerExitHandler(BaseEventData eventData)
        {
            
        }

    }
}

