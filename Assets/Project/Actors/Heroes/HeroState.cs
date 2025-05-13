using CMSystem;
using Project.Actors.Stats;
using UnityEngine;

namespace Project.Actors{

    public class HeroState{
        public HeroState(string id, CMSEntity model, HeroStats stats){
            m_id = id;
            m_stats = stats;
            m_model = model;
        }
        
        public string m_id;
        public HeroStats m_stats;                
        public CMSEntity m_model;

        public void TakeDamage(float amount)
        {
            m_stats.m_BaseStats.m_Health = Mathf.Clamp(m_stats.m_BaseStats.m_Health - amount, 0, m_stats.m_BaseStats.m_MaxHealth);
        }
    }
    
}
