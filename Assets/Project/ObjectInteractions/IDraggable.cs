using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            
            ConfigureDragBeginEntry();
            ConfigureDragEntry();
            ConfigureDragRealeseEntry();
        }

        private Interactable m_interactable;

        [HideInInspector] public bool isDragging;
        [HideInInspector] public Collider2D m_Collider;
        [HideInInspector] protected Camera g_MainCamera;


        #region DragDelegateHands
            Action<BaseEventData> OnDragBegin;
            Action<BaseEventData> OnDragRealese;
            Action<BaseEventData> OnDrag;
        #endregion
        
        private void ConfigureDragBeginEntry(){

            m_interactable.AddBeginDragListener((eventData) => OnDragBegin?.Invoke(eventData));
            m_interactable.AddBeginDragListener(DragBeginHandler);
        }
        private void ConfigureDragEntry(){
            m_interactable.AddDragListener((eventData) => OnDrag?.Invoke(eventData));
            m_interactable.AddDragListener(DragHandler);

        }
        private void ConfigureDragRealeseEntry(){
            m_interactable.AddEndDragListener((eventData) => OnDragRealese?.Invoke(eventData));
            m_interactable.AddEndDragListener(DragReleaseHandler);
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
