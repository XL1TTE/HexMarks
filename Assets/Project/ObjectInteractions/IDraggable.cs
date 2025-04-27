using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Project.ObjectInteractions
{
    [RequireComponent(typeof(Interactable))]
    public class IDraggable : MonoBehaviour
    {
  
        void Start()
        {
            m_Collider = gameObject.GetComponent<Collider2D>();
            g_MainCamera = Camera.main;

            m_interactable = GetComponent<Interactable>();
            
            EnableDragBehaviour();
        }

        private Interactable m_interactable;

        [HideInInspector] public bool isDragging;
        [HideInInspector] public Collider2D m_Collider;
        [HideInInspector] protected Camera g_MainCamera;
        
        public event UnityAction NotifyDragBegin;
        public event UnityAction NotifyDragEnd;

        void OnDestroy()
        {
            // Remove all listeners
            DisableDragBehaviour();
        }
        
        private void EnableDragBehaviour(){
            m_interactable.AddBeginDragListener(DragBeginHandler);
            m_interactable.AddBeginDragListener((eventData) => NotifyDragBegin?.Invoke());
            m_interactable.AddDragListener(DragHandler);
            m_interactable.AddEndDragListener((eventData) => NotifyDragEnd?.Invoke());
            m_interactable.AddEndDragListener(DragReleaseHandler);
        }
        
        [ContextMenu("DisableDrag")]
        private void DisableDragBehaviour(){
            m_interactable.RemoveBeginDragListener(DragBeginHandler);
            m_interactable.RemoveBeginDragListener((eventData) => NotifyDragBegin?.Invoke());
            m_interactable.RemoveDragListener(DragHandler);
            m_interactable.RemoveEndDragListener((eventData) => NotifyDragEnd?.Invoke());
            m_interactable.RemoveEndDragListener(DragReleaseHandler);
        }
        
           
        #region DragHandlers
            
            protected virtual void DragBeginHandler(BaseEventData eventData){
                isDragging = true;
            }
            protected virtual void DragHandler(BaseEventData eventData){
                
            }
            protected virtual void DragReleaseHandler(BaseEventData eventData){
                isDragging = false;
            }
            
        #endregion
    }
}
