using System;
using DG.Tweening;
using Project.ObjectInteractions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Project.Cards
{
    [RequireComponent(typeof(Interactable), typeof(CardView))]
    public class CardInteraction : MonoBehaviour
    {
        void Awake()
        {
            m_interactable = GetComponent<Interactable>();
            m_cardView = GetComponent<CardView>();

            m_cardView.AddDragBeginListener(DisableInteractions);
            m_cardView.AddDragEndListener(EnableInteractions);

            ConfigureCardAnimations();
            EnableInteractions();
        }

        void Start()
        {
            m_CardDefaultScale = transform.localScale;
        }

        private UnityAction PointerEnterHandlers;
        private UnityAction PointerExitHandlers;
        private UnityAction PointerClickHandlers;
        
        
        private Interactable m_interactable;
        private CardView m_cardView;
        
        
        #region Animations Settings
            Tween m_ScaleTween;
            Vector3 m_CardDefaultScale;
        
            [Header("Animation Settings")]
            [SerializeField, Range(0.1f, 1.5f)] float m_OnHoverScaleDuration;
            [SerializeField, Range(1f, 2f)] float m_OnHoverScalePower;
        #endregion
        
        
        void OnDestroy()
        {
            PointerClickHandlers = null;
            PointerEnterHandlers = null;
            PointerExitHandlers = null;

            m_cardView.RemoveDragBeginListener(DisableInteractions);
            m_cardView.RemoveDragEndListener(EnableInteractions);
        }


        #region Callbacks Setup
        
        private void EnableInteractions(){
            m_interactable.AddPointerClickListener(OnPointerClicked);
            m_interactable.AddPointerEnterListener(OnPointerEnter);
            m_interactable.AddPointerExitListener(OnPointerExit);
        }
        private void DisableInteractions(){
            m_interactable.RemovePointerClickListener(OnPointerClicked);
            m_interactable.RemovePointerEnterListener(OnPointerEnter);
            m_interactable.RemovePointerExitListener(OnPointerExit);
        }       

        #endregion

        #region Handlers

        private void OnPointerClicked(BaseEventData eventData)
        {
            PointerClickHandlers?.Invoke();
        }
        private void OnPointerEnter(BaseEventData eventData)
        {
            PointerEnterHandlers?.Invoke();
        }

        private void OnPointerExit(BaseEventData eventData)
        {
            PointerExitHandlers?.Invoke();
        }


        #endregion

        #region Card Animations

        private void ConfigureCardAnimations()
        {
            PointerEnterHandlers += CardPointerHover;
            PointerExitHandlers += CardPointerUnHover;
        }

        private void CardPointerHover(){
            m_cardView.SetSortingOrder(999);

            m_cardView.ChangeColor(Color.white);
            
            if(m_ScaleTween != null){m_ScaleTween.Kill();}
            
            m_ScaleTween = transform.DOScale(transform.localScale * m_OnHoverScalePower, m_OnHoverScaleDuration);
        }
        private void CardPointerUnHover(){
            
            m_cardView.SetSortingOrder(1);
            
            m_cardView.ChangeColor(Color.gray);

            if (m_ScaleTween != null) { m_ScaleTween.Kill(); }
            
            m_ScaleTween = transform.DOScale(m_CardDefaultScale, m_OnHoverScaleDuration);
        }
        #endregion

    }
}

