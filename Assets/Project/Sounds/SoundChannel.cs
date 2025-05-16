using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Sound
{
    [CreateAssetMenu(fileName = "SoundChannel", menuName = "XL1TTE/Sounds")]
    public class SoundChannel : ScriptableObject
    {
        private UnityEvent<AudioClip, float> m_requests;
        
        public void Subscribe(UnityAction<AudioClip, float> speaker){
            if(m_requests.GetPersistentEventCount() != 0){
                m_requests.RemoveAllListeners();
            }
            m_requests.AddListener(speaker);
        }
        
        private float m_volume = 1.0f;
        public void SetVolume(float value) => m_volume = Mathf.Clamp(value, 0.0f, 1.0f);
    
        public void PlaySound(AudioClip clip){
            m_requests.Invoke(clip, m_volume);
        }
    }
}
