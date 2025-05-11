using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Utilities{
    public static class FloatingIconUtility
    {
        static FloatingIconUtility()
        {
            var poolObject = new GameObject("WorldIconsPool");
            Object.DontDestroyOnLoad(poolObject);
            m_worldPoolTransform = poolObject.transform;

            var canvasPoolObject = new GameObject("CanvasIconsPool");
            Object.DontDestroyOnLoad(canvasPoolObject);
            m_canvasPoolTransform = canvasPoolObject.transform;
        }

        private static Queue<GameObject> m_worldIconPool = new();
        private static Queue<GameObject> m_canvasIconPool = new();
        private static Transform m_worldPoolTransform;
        private static Transform m_canvasPoolTransform;
        private static Vector3 m_DefaultWorldIconScale = Vector3.one;
        private static Vector3 m_DefaultCanvasIconScale = Vector3.one;

        public static SpriteRenderer ShowWorldIcon(
            Sprite icon,
            Vector3 position,
            Transform parent = null,
            Color? color = null,
            Vector3? scale = null)
        {
            GameObject iconObj;
            SpriteRenderer spriteComponent;

            if (m_worldIconPool.Count > 0)
            {
                iconObj = m_worldIconPool.Dequeue();
                iconObj.SetActive(true);
                spriteComponent = iconObj.GetComponent<SpriteRenderer>();
            }
            else
            {
                iconObj = new GameObject("WorldSpaceIcon");
                spriteComponent = iconObj.AddComponent<SpriteRenderer>();

                if (m_DefaultWorldIconScale == Vector3.zero)
                {
                    m_DefaultWorldIconScale = iconObj.transform.localScale;
                }
            }

            iconObj.transform.SetParent(parent);
            iconObj.transform.position = position;
            iconObj.transform.localScale = scale ?? m_DefaultWorldIconScale;

            spriteComponent.sprite = icon;
            spriteComponent.color = color ?? Color.white;

            return spriteComponent;
        }

        public static Image ShowCanvasIcon(
            Sprite icon,
            Vector3 position,
            RectTransform parentCanvas,
            Color? color = null,
            Vector3? scale = null)
        {
            if (parentCanvas == null)
            {
                Debug.LogError("Parent canvas cannot be null for canvas icons");
                return null;
            }

            GameObject iconObj;
            Image imageComponent;

            if (m_canvasIconPool.Count > 0)
            {
                iconObj = m_canvasIconPool.Dequeue();
                iconObj.SetActive(true);
                imageComponent = iconObj.GetComponent<Image>();
            }
            else
            {
                iconObj = new GameObject("CanvasIcon");
                imageComponent = iconObj.AddComponent<Image>();
                imageComponent.raycastTarget = false;

                if (m_DefaultCanvasIconScale == Vector3.zero)
                {
                    m_DefaultCanvasIconScale = iconObj.transform.localScale;
                }
            }

            iconObj.transform.SetParent(parentCanvas);
            iconObj.transform.localPosition = position;
            iconObj.transform.localScale = scale ?? m_DefaultCanvasIconScale;

            imageComponent.sprite = icon;
            imageComponent.color = color ?? Color.white;
            imageComponent.preserveAspect = true;

            return imageComponent;
        }

        public static void HideWorldIcon(SpriteRenderer icon)
        {
            var iconObj = icon.gameObject;
            iconObj.SetActive(false);
            iconObj.transform.SetParent(m_worldPoolTransform);
            iconObj.transform.localScale = m_DefaultWorldIconScale;
            m_worldIconPool.Enqueue(iconObj);
        }

        public static void HideCanvasIcon(Image icon)
        {
            var iconObj = icon.gameObject;
            iconObj.SetActive(false);
            iconObj.transform.SetParent(m_canvasPoolTransform);
            iconObj.transform.localScale = m_DefaultCanvasIconScale;
            m_canvasIconPool.Enqueue(iconObj);
        }
    }
}
