using System;
using System.Collections;
using Project.EventBus;
using Project.EventBus.Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    public class UINotification : MonoBehaviour
    {
        void Awake()
        {
            gameObject.SetActive(false);
        }
        [Inject]
        private void Construct(SignalBus a_signalBus){
            a_signalBus.Subscribe<BattleStateChangedSignal>(ShowNotification);   
        }    
        [SerializeField] TextMeshProUGUI m_NotificationText;
        [SerializeField] float m_NotificationDuration;
        
        public void ShowNotification(BattleStateChangedSignal signal){
            gameObject.SetActive(true);
            StartCoroutine(ShowNotificationCoroutine(signal.m_StateChangedMessage, m_NotificationDuration));
        }
        
        private void ShowText(string a_message) => m_NotificationText.text = a_message;
        private void ClearText() => m_NotificationText.text = String.Empty;
    
        public IEnumerator ShowNotificationCoroutine(string a_message, float a_duration){
            ShowText(a_message);
            yield return new WaitForSeconds(a_duration);
            ClearText();

            gameObject.SetActive(false);
        }
    }
}
