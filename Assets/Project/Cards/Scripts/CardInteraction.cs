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
            EventTrigger.Entry entry = m_interactable.m_PointerEnterEntry;
            entry.callback.AddListener((eventData) => OnPointerEnter?.Invoke(eventData));
            entry.callback.AddListener((eventData) => action?.Invoke(eventData));
        }

        private void ConfigurePointerExitEntry(Action<BaseEventData> action)
        {
            EventTrigger.Entry entry = m_interactable.m_PointerExitEntry;

            entry.callback.AddListener((eventData) => OnPointerExit?.Invoke(eventData));
            entry.callback.AddListener((eventData) => action?.Invoke(eventData));
        }

    }
}

