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
        
        public IEnumerator ShowNotification(string message, float duration, Color backgroundColor){
            gameObject.SetActive(true);
            
            m_BackGround.color = backgroundColor;
            
            yield return ShowNotificationCoroutine(message, duration);
        }
        
        private void ShowText(string a_message) => m_NotificationText.text = a_message;
        private void ClearText() => m_NotificationText.text = String.Empty;
    
        private IEnumerator ShowNotificationCoroutine(string a_message, float a_duration)
        {
            ShowText(a_message);
            yield return new WaitForSeconds(a_duration);
            ClearText();

            gameObject.SetActive(false);
        }
    }
}
