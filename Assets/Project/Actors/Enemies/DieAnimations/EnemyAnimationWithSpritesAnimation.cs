using System;
using System.Collections;
using UnityEngine;
using XL1TTE.Animator;

namespace Project.Enemies{
    
    [Serializable]
    public class EnemyAnimationWithSpritesAnimation : BaseEnemyAnimation
    {
        [SerializeField] private Sprite[] m_Sprites;
        [SerializeField, Range(50, 1000)] private float m_FrameDuration = 100;
        
        public override xlAnimation GetAnimation(EnemyView enemyView)
        {
            var animation = enemyView.GetRenderer().ToFrameAnimation(m_Sprites, m_FrameDuration);  
            
            return animation;      
        }
    }
}
