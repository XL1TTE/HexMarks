using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.ObjectInteractions
{
    [RequireComponent(typeof(IDraggable))]
    public class IDropable: MonoBehaviour{

        void Start()
        {
            m_draggable = GetComponent<IDraggable>();

            ConfigureDropEntry();
        }

        void OnEnable()
        {
            g_mainCamera = Camera.main;
        }

        IDraggable m_draggable;
        Camera g_mainCamera;
        [SerializeField] LayerMask m_DropMask;


        void OnDestroy()
        {
            m_draggable.NotifyDragEnd -= OnItemDroping;
        }

        void ConfigureDropEntry(){
            m_draggable.NotifyDragEnd += OnItemDroping;
        }

        public virtual void OnItemDroping()
        {
            Vector2 mousePosition = g_mainCamera.ScreenToWorldPoint(Input.mousePosition);
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
