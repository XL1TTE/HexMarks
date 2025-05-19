using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        void Awake()
        {
            m_audioSource = GetComponent<AudioSource>();
            
            foreach(var channel in m_SoundChannels){
                channel.Subscribe(PlaySound);
            }
        }
        [SerializeField] List<SoundChannel> m_SoundChannels;
        
        private AudioSource m_audioSource;
        
        private void PlaySound(AudioClip clip, float volume){
            m_audioSource.PlayOneShot(clip, volume);
        }
    }
}
