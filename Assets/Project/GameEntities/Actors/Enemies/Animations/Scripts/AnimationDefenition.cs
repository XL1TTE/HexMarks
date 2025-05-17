using System;
using System.Collections;
using System.Collections.Generic;
using Project.Enemies;
using Project.Sound;
using UnityEngine;
using XL1TTE.Animator;

namespace Enemies.Animations{
    
    [Serializable]
    public class AnimationDefenition
    {
        [Header("Animation")]
        [SerializeField] private Sprite[] m_Sprites;
        [SerializeField, Range(50, 1000)] private float m_FrameDuration = 100;
        
        [Header("Animation Settings")]
        [SerializeField] List<AnimationWithSFXFrameSettings> m_settings;
        
        [Header("SFX channel")]
        [SerializeField] SoundChannel SFX_Channel;

        public xlAnimation GetAnimation(EnemyView enemy)
        {
            var animation = enemy.GetRenderer().ToFrameAnimation(m_Sprites, m_FrameDuration);  
            
            foreach(var setting in m_settings){
            
                //SFX
                animation.AddFrameCallback(setting.m_FrameIndex, () => {
                    foreach (var item in setting.SFX)
                    {
                        SFX_Channel.PlaySound(item);
                    }
                });
            }
            
            return animation;      
        }
    }
}
