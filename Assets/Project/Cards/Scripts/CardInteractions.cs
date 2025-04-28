using System;
using DG.Tweening;
using Project.ObjectInteractions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Project.Cards
{
    
    [RequireComponent(typeof(Interactable))]
    public class CardInteractions : InteractionsBase
    {
        void Start()
        {
            m_CardDefaultScale = m_subject.GetTransform().localScale;
            m_CardDefaultColor = m_subject.GetRenderer().color;

            EnableDefaultInteractions();

            m_interactable.AddBeginDragListener(OnDragBegin);
            m_interactable.AddEndDragListener(OnDragEnd);
        }

        void OnDisable()
        {
            SetCardStateToDefault();
            
        }

        void OnDestroy()
        {
            m_interactable.RemoveBeginDragListener(OnDragBegin);
            m_interactable.RemoveEndDragListener(OnDragEnd);
        }

        #region Animations Settings
        Tween m_ScaleTween;
            Vector3 m_CardDefaultScale;
            Color m_CardDefaultColor;
        
            [Header("Animation Settings")]
            [SerializeField, Range(0.1f, 1.5f)] float m_OnHoverScaleDuration;
            [SerializeField, Range(1f, 2f)] float m_OnHoverScalePower;
        #endregion
        
        
        private void EnableDefaultInteractions(){
            EnableOnHoverHighlight();
            EnableCardScaleUpOnHover();
        }
        
        public void SetCardStateToDefault(){
            m_subject.GetTransform().localScale = m_CardDefaultScale;
            ChangeColor(m_CardDefaultColor);
            SetSortingOrder(0);
        }
        
        
        private void OnDragBegin(BaseEventData eventData = null){
            m_subject.GetCollider().enabled = false;
        }
        
        private void OnDragEnd(BaseEventData eventData = null){
            m_subject.GetCollider().enabled = true;
        }

        #region Hover Behaviour
        [SerializeField] private Color OnHoverColor;
            public void SetOnHoverColor(Color a_color) => OnHoverColor = a_color;
            
            public void EnableOnHoverHighlight() {
                m_interactable.AddPointerEnterListener(HighlightCard);
                m_interactable.AddPointerExitListener(UnHighlightCard);
            }
                
            public void DisableOnHoverHighlight() {
                m_interactable.RemovePointerEnterListener(HighlightCard);
                m_interactable.RemovePointerExitListener(UnHighlightCard);
            }
            
            public void EnableCardScaleUpOnHover(){
                m_interactable.AddPointerEnterListener(ScaleUpCard);
                m_interactable.AddPointerExitListener(ScaleDownCard);
            }
            public void DisableCardScaleUpOnHover(){
                m_interactable.RemovePointerEnterListener(ScaleUpCard);
                m_interactable.RemovePointerExitListener(ScaleDownCard);
            }
        #endregion

        private void HighlightCard(BaseEventData eventData = null)
        {
            SetSortingOrder(999);

            ChangeColor(OnHoverColor);
        }
        private void UnHighlightCard(BaseEventData eventData = null)
        {
            SetSortingOrder(0);

            ChangeColor(m_CardDefaultColor);
        }
        private void ScaleUpCard(BaseEventData eventData = null)
        {
            if (m_ScaleTween != null) { m_ScaleTween.Kill(); }

            SetSortingOrder(999);
            var s_transform = m_subject.GetTransform();

            m_ScaleTween = s_transform.DOScale(s_transform.localScale * m_OnHoverScalePower, m_OnHoverScaleDuration);
        }
        private void ScaleDownCard(BaseEventData eventData = null)
        {
            if (m_ScaleTween != null) { m_ScaleTween.Kill(); }

            SetSortingOrder(0);

            var s_transform = m_subject.GetTransform();

            m_ScaleTween = s_transform.DOScale(m_CardDefaultScale, m_OnHoverScaleDuration);
        }
        
    }
}

