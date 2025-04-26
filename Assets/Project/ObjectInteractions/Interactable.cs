using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.ObjectInteractions
{
    [RequireComponent(typeof(EventTrigger), typeof(BoxCollider2D))]
    public class Interactable : MonoBehaviour
    {
        void Awake()
        {
            m_eventTrigger = GetComponent<EventTrigger>();
            AddEntries();
        }
        
        EventTrigger m_eventTrigger;
        
        #region TriggerEntries
            [HideInInspector] public EventTrigger.Entry m_PointerEnterEntry;
            [HideInInspector] public EventTrigger.Entry m_PointerExitEntry;
            [HideInInspector] public EventTrigger.Entry m_PointerUpEntry;
            [HideInInspector] public EventTrigger.Entry m_PointerDownEntry;
            [HideInInspector] public EventTrigger.Entry m_DragBeginEntry;
            [HideInInspector] public EventTrigger.Entry m_DragEndEntry;
            [HideInInspector] public EventTrigger.Entry m_DragEntry;                 
        #endregion

        private void AddEntries(){
            m_PointerEnterEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            m_eventTrigger.triggers.Add(m_PointerEnterEntry);

            m_PointerExitEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
            m_eventTrigger.triggers.Add(m_PointerExitEntry);

            m_PointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
            m_eventTrigger.triggers.Add(m_PointerUpEntry);

            m_PointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
            m_eventTrigger.triggers.Add(m_PointerDownEntry);

            m_DragBeginEntry = new EventTrigger.Entry { eventID = EventTriggerType.BeginDrag };
            m_eventTrigger.triggers.Add(m_DragBeginEntry);

            m_DragEndEntry = new EventTrigger.Entry { eventID = EventTriggerType.EndDrag };
            m_eventTrigger.triggers.Add(m_DragEndEntry);

            m_DragEntry = new EventTrigger.Entry { eventID = EventTriggerType.Drag };
            m_eventTrigger.triggers.Add(m_DragEntry);
        }
        
    }
}
