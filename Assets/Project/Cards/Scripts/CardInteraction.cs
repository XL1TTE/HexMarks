using System;
using Project.ObjectInteractions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Cards
{
    [RequireComponent(typeof(Interactable), typeof(CardView))]
    public class CardInteraction : MonoBehaviour
    {
        void Awake()
        {
            m_interactable = GetComponent<Interactable>();
        }
        
        public void Init(CardController a_controller){
            m_cardController = a_controller;
            
            if (m_cardController == null) { return; }

            ConfigurePointerEnterEntry(m_cardController.PointerEnterHandler);
            ConfigurePointerExitEntry(m_cardController.PointerExitHandler);
        }

        private Interactable m_interactable;
        private CardController m_cardController;


        #region RemoteEventHands
            Action<BaseEventData> OnPointerEnter;
            Action<BaseEventData> OnPointerExit;
        #endregion

        private void ConfigurePointerEnterEntry(Action<BaseEventData> action)
        {
            m_interactable.AddPointerEnterListener((eventData) => OnPointerEnter?.Invoke(eventData));
            m_interactable.AddPointerEnterListener((eventData) => action?.Invoke(eventData));
        }

        private void ConfigurePointerExitEntry(Action<BaseEventData> action)
        {
            m_interactable.AddPointerExitListener((eventData) => OnPointerExit?.Invoke(eventData));
            m_interactable.AddPointerExitListener((eventData) => action?.Invoke(eventData));
        }

    }
}

