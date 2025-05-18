using System.Collections.Generic;
using Project.Utilities.Extantions;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace Project.Utilities.Tooltips{
    
    public static class FloatingTextUtility{
        static FloatingTextUtility(){
            var poolObject = new GameObject("WorldSpaceTextPool");
            
            Object.DontDestroyOnLoad(poolObject);
            m_poolTransform = poolObject.transform;
        }
        
        private static Queue<GameObject> m_pool = new();
        private static Transform m_poolTransform;

        
        private static Vector3 m_DefaultTextScale;
        private static TMP_FontAsset m_DefaultFont = $"UI/Fonts/Kaph".LoadResource<TMP_FontAsset>();
        
        public static TMP_Text ShowWorldText(
            string text,
            Vector3 position,
            Transform parent = null,
            int fontSize = 12,
            Color? color = null,
            string fontName = null
        )
        {
            GameObject text_obj;
            TMP_Text textComponent;
            
            if (m_pool.Count > 0){text_obj = m_pool.Dequeue(); text_obj.SetActive(true); textComponent = text_obj.GetComponent<TextMeshPro>();}
            else
            { 
                text_obj = new GameObject("WorldSpaceText");
                
                text_obj.layer = LayerMask.NameToLayer("VFX");
                
                textComponent = text_obj.AddComponent<TextMeshPro>();
                
                var sorting = text_obj.AddComponent<SortingGroup>();
                sorting.sortingOrder = 999;

                if (m_DefaultTextScale == Vector3.zero){
                    m_DefaultTextScale = text_obj.transform.localScale;
                }
            }

            TMP_FontAsset font = $"Fonts/{fontName}".LoadResource<TMP_FontAsset>();

            text_obj.transform.SetParent(parent);
            text_obj.transform.position = position;


            textComponent.text = text;
            textComponent.fontSize = fontSize;
            textComponent.color = color ?? Color.white;
            textComponent.alignment = TextAlignmentOptions.Center;


            textComponent.font = font ?? m_DefaultFont;

            return textComponent;
        }
        
        public static void HideWorldText(TMP_Text text){
            text.gameObject.SetActive(false);
            text.gameObject.transform.SetParent(m_poolTransform);
            
            text.gameObject.transform.localScale = m_DefaultTextScale;

            m_pool.Enqueue(text.gameObject);
        }

    }
    
}
