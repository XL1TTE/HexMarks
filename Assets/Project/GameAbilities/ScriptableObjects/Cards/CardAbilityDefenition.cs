using System;
using System.Collections;
using Project.Cards;
using Project.Sound;
using UnityEngine;
using XL1TTE.Animator;

namespace XL1TTE.GameAbilities
{


    [CreateAssetMenu(fileName = "CardAbilityDef", menuName = "XL1TTE/Cards/AbilityDef")]
    public class CardAbilityDefenition : ScriptableObject
    {
        [Header("Animation")]
        [SerializeField] private Sprite[] m_CardAnimation;
        [SerializeField, Range(100, 1000)] private float m_FrameDuration = 100;

        [Header("Animation")]
        [SerializeField] CardAbilityFrameDefenition[] m_FrameSettings;

        [SerializeField] private SoundChannel SFX_Channel;
        
        public FrameAnimation GetAbility(CardView card){
            
            FrameAnimation anim = card.GetRenderer().ToFrameAnimation(m_CardAnimation, m_FrameDuration);
            
            for (int i = 0; i < m_FrameSettings.Length; i++)
            {
                var frame_settings = m_FrameSettings[i];
                if (frame_settings.m_FrameTriggerIndex >= m_CardAnimation.Length) { return anim; }

                IEnumerator CardActionCallback()
                {
                    foreach (var a in frame_settings.m_Actions)
                    {
                        yield return a.Execute(card);
                    }
                }

                // Enemy actions
                anim.AddFrameCallback(frame_settings.m_FrameTriggerIndex, CardActionCallback);

                // SFX
                anim.AddFrameCallback(frame_settings.m_FrameTriggerIndex,
                    () =>
                    {
                        foreach (var sfx in frame_settings.m_SFX)
                        {
                            SFX_Channel.PlaySound(sfx);
                        }
                    });
            }
            return anim;
        }

    }
}
