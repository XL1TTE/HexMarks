using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class UINotification : MonoBehaviour
    {
        void Awake()
        {
            if(current == null){
                current = this;
            }
            gameObject.SetActive(false);
        }
        
        
        public static UINotification current = null;

        [SerializeField] TextMeshProUGUI m_NotificationText;
        [SerializeField] Image m_BackGround;
        
        public void ShowNotification(string message,  Color backgroundColor){
            gameObject.SetActive(true);

            ShowText(message);
            m_BackGround.color = backgroundColor;
            
        }
        
        private void ShowText(string a_message) => m_NotificationText.text = a_message;
        private void ClearText() => m_NotificationText.text = String.Empty;
    
        public void HideNotification(){
            gameObject.SetActive(false);
            ClearText();
        }
    }
}
