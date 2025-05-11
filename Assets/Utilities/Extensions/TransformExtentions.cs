using UnityEngine;

namespace Project.Utilities.Extantions{
    public static class TransformExtentions{
        public static float GetHeight(this Transform parent)
        {
            var obj = parent.gameObject;

            Renderer renderer;
            if (!obj.TryGetComponent<Renderer>(out renderer))
            {
                renderer = obj.GetComponentInChildren<Renderer>();
            }

            float height = 0f;

            if (renderer != null)
            {
                height = renderer.bounds.size.y;
            }
            else
            {
                Collider2D collider;
                if (!obj.TryGetComponent<Collider2D>(out collider))
                {
                    collider = obj.GetComponentInChildren<Collider2D>();
                }
                if (collider != null)
                {
                    height = collider.bounds.size.y;
                }
            }

            return height;
        }
        public static float GetWidth(this Transform parent)
        {
            var obj = parent.gameObject;

            Renderer renderer;
            if (!obj.TryGetComponent<Renderer>(out renderer))
            {
                renderer = obj.GetComponentInChildren<Renderer>();
            }

            float width = 0f;

            if (renderer != null)
            {
                width = renderer.bounds.size.x;
            }
            else
            {
                Collider2D collider;
                if (!obj.TryGetComponent<Collider2D>(out collider))
                {
                    collider = obj.GetComponentInChildren<Collider2D>();
                }
                if (collider != null)
                {
                    width = collider.bounds.size.x;
                }
            }

            return width;
        }
    }
}
