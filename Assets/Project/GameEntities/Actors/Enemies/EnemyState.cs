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

        internal float TakeDamage(float amount){
            var temp = m_Stats.m_BaseStats.m_Health;

            m_Stats.m_BaseStats.m_Health = Mathf.Clamp(m_Stats.m_BaseStats.m_Health - amount, 0, m_Stats.m_BaseStats.m_MaxHealth);
            
            return temp - m_Stats.m_BaseStats.m_Health;
        }

        internal float TakeHeal(float amount){
            var temp = m_Stats.m_BaseStats.m_Health;

            m_Stats.m_BaseStats.m_Health = Mathf.Clamp(m_Stats.m_BaseStats.m_Health + amount, 0, m_Stats.m_BaseStats.m_MaxHealth);

            return m_Stats.m_BaseStats.m_Health - temp;
        }
        
        public float GetMaxHealth() => m_Stats.m_BaseStats.m_MaxHealth;
        public float GetCurrentHealth() => m_Stats.m_BaseStats.m_Health;
        public float GetEnemyDamage() => m_Stats.m_Damage;
        
        public int GetInitiaive() => m_Stats.m_BaseStats.m_Initiative;
        
    }
    
}
