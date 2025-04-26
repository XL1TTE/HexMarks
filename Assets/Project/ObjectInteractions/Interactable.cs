using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Project.ObjectInteractions
{
    [RequireComponent(typeof(EventTrigger), typeof(BoxCollider2D))]
    public class Interactable : MonoBehaviour
    {
        
    #region Event Entries
        private readonly EventTrigger.Entry m_pointerEnterEntry = new() { eventID = EventTriggerType.PointerEnter };
        private readonly EventTrigger.Entry m_pointerExitEntry = new() { eventID = EventTriggerType.PointerExit };
        private readonly EventTrigger.Entry m_pointerDownEntry = new() { eventID = EventTriggerType.PointerDown };
        private readonly EventTrigger.Entry m_pointerUpEntry = new() { eventID = EventTriggerType.PointerUp };
        private readonly EventTrigger.Entry m_beginDragEntry = new() { eventID = EventTriggerType.BeginDrag };
        private readonly EventTrigger.Entry m_dragEntry = new() { eventID = EventTriggerType.Drag };
        private readonly EventTrigger.Entry m_endDragEntry = new() { eventID = EventTriggerType.EndDrag };
    #endregion

    private EventTrigger m_eventTrigger;

    protected virtual void Awake()
    {
        m_eventTrigger = GetComponent<EventTrigger>();
        InitializeEventSystem();
    }

    private void InitializeEventSystem()
    {
        var triggers = new List<EventTrigger.Entry>
        {
            m_pointerEnterEntry,
            m_pointerExitEntry,
            m_pointerDownEntry,
            m_pointerUpEntry,
            m_beginDragEntry,
            m_dragEntry,
            m_endDragEntry
        };
        
        m_eventTrigger.triggers = triggers;
    }

    #region Add Listeners API
    public void AddPointerEnterListener(UnityAction<BaseEventData> action) => AddListener(m_pointerEnterEntry, action);
    public void AddPointerExitListener(UnityAction<BaseEventData> action) => AddListener(m_pointerExitEntry, action);
    public void AddPointerDownListener(UnityAction<BaseEventData> action) => AddListener(m_pointerDownEntry, action);
    public void AddPointerUpListener(UnityAction<BaseEventData> action) => AddListener(m_pointerUpEntry, action);
    public void AddBeginDragListener(UnityAction<BaseEventData> action) => AddListener(m_beginDragEntry, action);
    public void AddDragListener(UnityAction<BaseEventData> action) => AddListener(m_dragEntry, action);
    public void AddEndDragListener(UnityAction<BaseEventData> action) => AddListener(m_endDragEntry, action);
    #endregion

    #region Remove Listeners API
    public void RemovePointerEnterListener(UnityAction<BaseEventData> action) => RemoveListener(m_pointerEnterEntry, action);
    public void RemovePointerExitListener(UnityAction<BaseEventData> action) => RemoveListener(m_pointerExitEntry, action);
    public void RemovePointerDownListener(UnityAction<BaseEventData> action) => RemoveListener(m_pointerDownEntry, action);
    public void RemovePointerUpListener(UnityAction<BaseEventData> action) => RemoveListener(m_pointerUpEntry, action);
    public void RemoveBeginDragListener(UnityAction<BaseEventData> action) => RemoveListener(m_beginDragEntry, action);
    public void RemoveDragListener(UnityAction<BaseEventData> action) => RemoveListener(m_dragEntry, action);
    public void RemoveEndDragListener(UnityAction<BaseEventData> action) => RemoveListener(m_endDragEntry, action);
    #endregion
    
    #region Helper Methods
        private static void AddListener(EventTrigger.Entry entry, UnityAction<BaseEventData> action)
        {
            if (entry == null || action == null) return;
            entry.callback.AddListener(action.Invoke);
        }

        private static void RemoveListener(EventTrigger.Entry entry, UnityAction<BaseEventData> action)
        {
            if (entry == null || action == null) return;
            entry.callback.RemoveListener(action.Invoke);
        }
    #endregion
    }
}
