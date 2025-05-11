using CMSystem;
using Project.Actors.Stats;

namespace Project.Actors{

    public class HeroState{
        public HeroState(HeroStats stats, CMSEntity model){
            m_Stats = stats;
            m_Model = model;
        }
        
        private HeroStats m_Stats;
        private CMSEntity m_Model;
        
        public int GetInitiative() => m_Stats.m_BaseStats.m_Initiative;
        
        public float GetCurrentHealth() => m_Stats.m_BaseStats.m_Health;
        public float GetMaxHealth() => m_Stats.m_BaseStats.m_MaxHealth;
    }
    
}
