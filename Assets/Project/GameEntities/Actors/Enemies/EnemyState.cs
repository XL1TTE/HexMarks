using CMSystem;
using Enemies.Animations;
using Project.Actors.Stats;
using UnityEngine;
using XL1TTE.Animator;

namespace Project.Enemies{
    public class EnemyState
    {
        
        public EnemyState(
            EnemyStats stats, 
            CMSEntity model)
        {
            m_Stats = stats;

            m_Model = model;
        }
        
        private EnemyStats m_Stats;
        private CMSEntity m_Model;
        public CMSEntity GetModel() => m_Model;
        
        private AnimationDefenition m_DieAnimation;
        public void SetDieAnimation(AnimationDefenition anim) => m_DieAnimation = anim;
        public xlAnimation GetDieAnimation(EnemyView view) => m_DieAnimation.GetAnimation(view);
        
        private AnimationDefenition m_IdleAnimation;
        public void SetIdleAnimation(AnimationDefenition anim) => m_IdleAnimation = anim;
        public xlAnimation GetIdleAnimation(EnemyView view) => m_IdleAnimation.GetAnimation(view);

        public void TakeDamage(float amount){
            m_Stats.m_BaseStats.m_Health = Mathf.Clamp(m_Stats.m_BaseStats.m_Health - amount, 0, m_Stats.m_BaseStats.m_MaxHealth);
        }
        public float GetMaxHealth() => m_Stats.m_BaseStats.m_MaxHealth;
        public float GetCurrentHealth() => m_Stats.m_BaseStats.m_Health;
        public float GetEnemyDamage() => m_Stats.m_Damage;
        
        public int GetInitiaive() => m_Stats.m_BaseStats.m_Initiative;
        
    }
    
}
