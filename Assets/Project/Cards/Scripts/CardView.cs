using System;
using System.Collections;
using Project.Layouts;
using Project.ObjectInteractions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Project.Cards{
    
    public class CardView : MonoBehaviour, IHaveInteractions
    {
        void Awake()
        {
            TryGetComponent(out m_draggable);
            TryGetComponent(out m_sorting);
            TryGetComponent(out m_Interactions);
        }
        public void Init(Card a_CardController){
            m_CardController = a_CardController;
        }
        private Card m_CardController;
        public Card GetCardController() => m_CardController;

        #region Dragable
        private IDraggable m_draggable;
        public void EnableDragging() => m_draggable.EnableDragBehaviour();
        public void DisableDragging() => m_draggable.DisableDragBehaviour();
        public bool IsDragging() => m_draggable.isDragging;
        #endregion

        #region Sorting
        private SortingGroup m_sorting;
        public SortingGroup GetSortingGroup() => m_sorting;
        #endregion


        public Transform GetTransform() => transform;

        #region Layouts
        private ICardLayout m_currentLayout;
        public void SetLayout(ICardLayout layout) => m_currentLayout = layout;
        public void LeaveLayout() => m_currentLayout?.Release(this);
        #endregion

        #region Drag Event Listeners
        public void AddDragBeginListener(UnityAction listener) => m_draggable.NotifyDragBegin += listener;
        public void AddDragEndListener(UnityAction listener) => m_draggable.NotifyDragEnd += listener;
        public void RemoveDragBeginListener(UnityAction listener) => m_draggable.NotifyDragBegin -= listener;
        public void RemoveDragEndListener(UnityAction listener) => m_draggable.NotifyDragEnd -= listener;
        #endregion

        public void AddPointerEnterListener(UnityAction<CardView> listener) => m_Interactions.NotifyPointerEnter += listener;
        public void AddPointerExitListener(UnityAction<CardView> listener) => m_Interactions.NotifyPointerExit += listener;
        public void RemovePointerEnterListener(UnityAction<CardView> listener) => m_Interactions.NotifyPointerEnter -= listener;
        public void RemovePointerExitListener(UnityAction<CardView> listener) => m_Interactions.NotifyPointerExit -= listener;

        #region Renderer
        [SerializeField] protected SpriteRenderer m_Renderer;

            public SpriteRenderer GetRenderer() => m_Renderer;
        #endregion

        #region CardInteractions
        private CardInteractions m_Interactions;
        public CardInteractions GetInteractions() => m_Interactions;
        public Collider2D GetCollider() => m_draggable.m_Collider;
        
        #endregion
        
        public IEnumerator GetCardUseSequence(){
            return m_CardController.GetCardUseSequence();
        }

        void OnDisable()
        {
            LeaveLayout();
        }
    }
}

