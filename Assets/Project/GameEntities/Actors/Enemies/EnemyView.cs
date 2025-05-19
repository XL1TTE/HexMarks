using System;
using Enemies.Animations;
using Project.Actors;
using UnityEngine;
using XL1TTE.Animator;

namespace Project.Enemies
{
    // May be taget inside enemy state as field and make some kind of setupable AI with 
    // pick target customizable neirones which will each provide best enemy by their opinion
    // and method finalPickTarget will grab chose with larger weight, then put it inside enemy 
    // target field. All target driven abilities will depends on that target field. Then in some 
    // abilities we could change ai field for needed (pick random ally for example to made
    // enemy ai to attack his ally.) 
    
    
    public class EnemyView : MonoBehaviour, ITargetable
    {

        public void Init(EnemyState state){
            m_state = state;
        }
        
        void OnDestroy()
        {
            StopIdleAnimation();
        }

        private EnemyState m_state;
        
        public EnemyState GetState() => m_state;
        
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        public SpriteRenderer GetRenderer() => m_spriteRenderer;


        #region Animations
        
        public FrameAnimation m_idleAnimation {get; private set;}
        public FrameAnimation m_attackAnimation { get; private set; }
        public FrameAnimation m_dieAnimation { get; private set; }

        public void SetIdleAnimation(FrameAnimation anim) => m_idleAnimation = anim;
        public void SetAttackAnimation(FrameAnimation anim) => m_attackAnimation = anim;
        public void SetDieAnimation(FrameAnimation anim) => m_dieAnimation = anim;


        public void StartIdleAnimation()
        {

            if (m_idleAnimation == null) { return; }
            m_idleAnimation.SetLoops(-1).Play();
        }
        public void StopIdleAnimation()
        {
            if (m_idleAnimation != null)
            {
                m_idleAnimation.Kill();
            }
        }

        #endregion


        public event Action<EnemyView> OnHealthChanged;

        public float TakeDamage(float amount)
        {
            var damageTaken = m_state.TakeDamage(amount);
            OnHealthChanged?.Invoke(this);
            return damageTaken;
        }

        public float TakeHeal(float amount)
        {
            var healTaken = m_state.TakeHeal(amount);
            OnHealthChanged?.Invoke(this);
            return healTaken;
        }

        public float GetCurrentHealth() => 
            m_state.GetCurrentHealth();
    }
}
