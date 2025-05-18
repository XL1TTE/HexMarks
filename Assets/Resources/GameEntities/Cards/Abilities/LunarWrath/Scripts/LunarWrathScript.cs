using System.Collections;
using System.Collections.Generic;
using Enemies.Animations;
using UnityEngine;
using XL1TTE.Animator;

namespace Project.Enemies.Abilities
{
    public class LunarWrathScript : MonoBehaviour
    {
        [SerializeField] AnimationDefenition m_IdleAnimation;
        [SerializeField] AnimationDefenition m_DisappearAnimation;
        [SerializeField] AnimationDefenition m_CastAnimation;

        public void OnEnable()
        {
            StartIdleAnimation();
        }
        void OnDisable()
        {
            StopAllCoroutines();
        }
        
        [SerializeField] private SpriteRenderer m_renderer;
        private xlAnimation m_idleAnim;
        private Coroutine m_idleCoroutine;
        
        private IEnumerator PlayIdleAnimation(){
            m_idleAnim = m_IdleAnimation.GetAnimation(m_renderer).SetLoops(-1);
            
            yield return m_idleAnim.Play().WaitForCompletion();
        }
        
        public void StartIdleAnimation(){
            if(m_idleCoroutine != null){
                StopIdleAnimation();
            }
            m_idleCoroutine = StartCoroutine(PlayIdleAnimation());
        }
        public void StopIdleAnimation(){
            if(m_idleAnim != null){
                m_idleAnim.Kill();
            }
            StopCoroutine(m_idleCoroutine);
        }
        
        public FrameAnimation GetCastAnimation(){
            return m_CastAnimation.GetAnimation(m_renderer);
        }
        
        public FrameAnimation GetDisappearAnimation(){
            return m_DisappearAnimation.GetAnimation(m_renderer);
        }
            
        public void DestroyGameObject(){
            Destroy(gameObject);
        }
    }
}
