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
            EventTrigger.Entry entry = m_interactable.m_DragBeginEntry;
            entry.callback.AddListener((eventData) => OnDragBegin?.Invoke(eventData));
            entry.callback.AddListener(DragBeginHandler);
        }
        private void ConfigureDragEntry(){
            EventTrigger.Entry entry = m_interactable.m_DragEntry;
            entry.callback.AddListener((eventData) => OnDrag?.Invoke(eventData));
            entry.callback.AddListener(DragHandler);

        }
        private void ConfigureDragRealeseEntry(){
            EventTrigger.Entry entry = m_interactable.m_DragEndEntry;
            entry.callback.AddListener((eventData) => OnDragRealese?.Invoke(eventData));
            entry.callback.AddListener(DragReleaseHandler);
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
