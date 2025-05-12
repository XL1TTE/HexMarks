using CMSystem;
using Project.Actors.Stats;
using UnityEngine;

namespace Project.Actors{

    public class HeroState{
        public HeroState(HeroStats stats, CMSEntity model){
            m_Stats = stats;
            m_Model = model;
        }
        
        private HeroStats m_Stats;
        private CMSEntity m_Model;
        
        public CMSEntity GetCMSModel() => m_Model; 
        
        public int GetInitiative() => m_Stats.m_BaseStats.m_Initiative;
        
        public float GetCurrentHealth() => m_Stats.m_BaseStats.m_Health;
        public float GetMaxHealth() => m_Stats.m_BaseStats.m_MaxHealth;

        public int GetHandCapacity() => m_Stats.m_MaxCardsInHand;

        public void TakeDamage(float amount)
        {
            m_Stats.m_BaseStats.m_Health = Mathf.Clamp(m_Stats.m_BaseStats.m_Health - amount, 0, m_Stats.m_BaseStats.m_MaxHealth);
        }
    }
    
}
