using TMPro;
using UnityEngine;

namespace Project.Utilities{
    public class ToolTip : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI Header;
        [SerializeField] public TextMeshProUGUI Message;
        [SerializeField] private Vector2 offset = new Vector2(10, -10);
        [SerializeField] private float screenMargin = 20f;

        [SerializeField] private RectTransform m_Rect;
        private void Awake()
        {
            m_Rect = GetComponent<RectTransform>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (gameObject.activeSelf)
            {
                UpdatePosition();
            }
        }

        private void UpdatePosition()
        {
            if (m_Rect == null)
            {
                Debug.LogError("RectTransform is missing!");
                return;
            }

            if (Header == null || Message == null)
            {
                Debug.LogError("Header or Message TextMeshProUGUI components are not assigned!");
                return;
            }

            Vector2 mousePos = Input.mousePosition;
            Vector2 size = m_Rect.sizeDelta;

            bool rightSide = mousePos.x + size.x + screenMargin < Screen.width;
            bool topSide = mousePos.y + size.y + screenMargin < Screen.height;

            m_Rect.pivot = new Vector2(
                rightSide ? 0 : 1,
                topSide ? 0 : 1
            );

            float posX = rightSide ?
                Mathf.Clamp(mousePos.x + offset.x, screenMargin, Screen.width - size.x - screenMargin) :
                Mathf.Clamp(mousePos.x - offset.x, screenMargin, Screen.width - screenMargin);

            float posY = topSide ?
                Mathf.Clamp(mousePos.y - offset.y, screenMargin, Screen.height - size.y - screenMargin) :
                Mathf.Clamp(mousePos.y + offset.y, screenMargin, Screen.height - screenMargin);

            m_Rect.position = new Vector2(posX, posY);
        }
    }

}
