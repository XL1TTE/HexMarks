using System;
using Project.ObjectInteractions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Project.Cards{
    public class CardView : MonoBehaviour
    {
        void Awake()
        {
            TryGetComponent(out m_draggable);
            TryGetComponent(out m_sorting);
        }
        public void Init(Card a_CardController){
            m_CardController = a_CardController;
        }
        private Card m_CardController;
        public Card GetCardController() => m_CardController;
        
        [SerializeField] private SpriteRenderer m_renderer;
        public void ChangeColor(Color a_color) => m_renderer.color = a_color;
        private IDraggable m_draggable;
        public bool IsDragging() => m_draggable.isDragging;

        private SortingGroup m_sorting;
        public void SetSortingOrder(int order) => m_sorting.sortingOrder = order;

        public void AddDragBeginListener(UnityAction listener) => m_draggable.NotifyDragBegin += listener;
        public void AddDragEndListener(UnityAction listener) => m_draggable.NotifyDragEnd += listener;
        public void RemoveDragBeginListener(UnityAction listener) => m_draggable.NotifyDragBegin -= listener;
        public void RemoveDragEndListener(UnityAction listener) => m_draggable.NotifyDragEnd -= listener;
    }
}

