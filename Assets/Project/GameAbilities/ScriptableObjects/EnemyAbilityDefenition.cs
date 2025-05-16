using System;
using System.Collections;
using System.Collections.Generic;
using Project.Enemies;
using Project.Sound;
using UnityEngine;
using XL1TTE.GameActions;

namespace XL1TTE.Animator{
    
    
    [Serializable]
    internal class EnemyAbilityFrameDefenition{
        public int m_FrameTriggerIndex;
        public List<AudioClip> m_SFX;
        [SerializeReference, SubclassSelector] public List<EnemyAction> m_Actions; 
    }
    
    [CreateAssetMenu(fileName = "EnemyAbility", menuName = "XL1TTE/EnemyAbilities/AbilityDef")]
    public class EnemyAbilityDefenition: ScriptableObject{
        
        [Header("Animation")]
        
        [SerializeField] private Sprite[] m_Animation;
        [SerializeField, Range(100, 1000)] private float m_FrameDuration = 100;
        
        [Header("Animation Settings")]
        [SerializeField] private EnemyAbilityFrameDefenition[] m_FrameSettings;
        
        [SerializeField] private SoundChannel SFX_Channel;
        
        public FrameAnimation GetAbility(EnemyView enemyView){
            var anim = enemyView.GetRenderer().ToFrameAnimation(m_Animation, m_FrameDuration);
            
            for(int i = 0; i < m_FrameSettings.Length; i++){
                var frame_settings = m_FrameSettings[i]; 
                if(frame_settings.m_FrameTriggerIndex >= m_Animation.Length){return anim;}
                
                IEnumerator EnemyActionCallback(){
                    foreach(var a in frame_settings.m_Actions){
                        yield return a.Execute(enemyView);
                    }
                }
                
                // Enemy actions
                anim.AddFrameCallback(frame_settings.m_FrameTriggerIndex, EnemyActionCallback);
                        
                // SFX
                anim.AddFrameCallback(frame_settings.m_FrameTriggerIndex, 
                    () => {
                        foreach(var sfx in frame_settings.m_SFX){
                            SFX_Channel.PlaySound(sfx);
                        }
                    });
            }
            return anim;
        }
    }
}
