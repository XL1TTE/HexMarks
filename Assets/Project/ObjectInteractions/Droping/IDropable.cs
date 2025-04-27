using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.ObjectInteractions
{
    [RequireComponent(typeof(Interactable))]
    public class IDropable: MonoBehaviour{

        void Start()
        {
            m_interactable = GetComponent<Interactable>();
            g_mainCamera = Camera.main;

            ConfigureDropEntry();
        }
        Interactable m_interactable;
        Camera g_mainCamera;
        [SerializeField] LayerMask m_DropMask;


        void OnDestroy()
        {
            m_interactable.RemoveEndDragListener(OnItemDroping);
        }

        void ConfigureDropEntry(){
            m_interactable.AddEndDragListener(OnItemDroping);
        }

        public virtual void OnItemDroping(BaseEventData eventData)
        {
            PointerEventData MouseEventData = eventData as PointerEventData;

            Vector2 mousePosition = g_mainCamera.ScreenToWorldPoint(MouseEventData.position);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, m_DropMask);

            if (hit.collider != null)
            {
                IDropHandler dropHandler = hit.collider.GetComponent<IDropHandler>();
                if (dropHandler != null)
                {
                    dropHandler.HandleDrop(gameObject);
                }
            }
        }
    }
}
