using System;
using Project.Layouts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Project.Cards{
    public class Card
    {
        public Card(CardModel a_model, CardView a_cardView){
            m_model = a_model;
            m_view = a_cardView;
            m_view.Init(this);
        }
        private CardModel m_model;
        private CardView m_view;

        #region View API
        public Transform GetViewTransform() => m_view.transform;
        public bool IsDragging() => m_view.IsDragging();
        
        public void AddDragBeginListener(UnityAction listener) => m_view.AddDragBeginListener(listener);
        public void AddDragEndListener(UnityAction listener) => m_view.AddDragEndListener(listener);
        public void RemoveDragBeginListener(UnityAction listener) => m_view.RemoveDragBeginListener(listener);
        public void RemoveDragEndListener(UnityAction listener) => m_view.RemoveDragEndListener(listener);
        #endregion

        private ICardLayout m_currentLayout;
        public void SetLayout(ICardLayout layout) => m_currentLayout = layout;
        public void LeaveLayout() => m_currentLayout?.Release(this);
    }
}

