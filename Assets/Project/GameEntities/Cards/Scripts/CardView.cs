using CMSystem;
using Project.Layouts;
using Project.ObjectInteractions;
using UnityEngine;
using UnityEngine.Events;
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

        public void AddPointerEnterListener(UnityAction<CardView> listener) => m_Interactions.NotifyPointerEnter += listener;
        public void AddPointerExitListener(UnityAction<CardView> listener) => m_Interactions.NotifyPointerExit += listener;
        public void RemovePointerEnterListener(UnityAction<CardView> listener) => m_Interactions.NotifyPointerEnter -= listener;
        public void RemovePointerExitListener(UnityAction<CardView> listener) => m_Interactions.NotifyPointerExit -= listener;

        #endregion


        #region Renderer
        [SerializeField] protected SpriteRenderer m_Renderer;
        public SpriteRenderer GetRenderer() => m_Renderer;
        #endregion

        #region CardInteractions
        private CardInteractions m_Interactions;
        public CardInteractions GetInteractions() => m_Interactions;
        public Collider2D GetCollider() => m_draggable.m_Collider;
        
        #endregion
        
        
        public void OnCardPlayed() => m_controller.PlayCard();
        
        private Card m_controller;
        public void Init(Card controller){
            m_controller = controller;
        }
        public string GetModelID() => m_controller.m_state.model.id;
        public CMSEntity GetModel() => m_controller.m_state.model;

        void OnDisable()
        {
            LeaveLayout();
        }
    }

    public class meta_ToolTip
    {
        public string card_name;
        public string card_desc;
    }
}

