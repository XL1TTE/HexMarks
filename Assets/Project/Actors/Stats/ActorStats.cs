
using System;

namespace Project.Actors.Stats{
    
    
    public interface IHealthStatsAccessor{
        float GetCurrentHealth();
        float GetMaxHealth();
    }
    
    [Serializable]
    public struct ActorStats
    {      
        public float m_Health;
        public float m_MaxHealth;        
        public int m_Initiative;
    }
    
    [Serializable]
    public struct EnemyStats
    {    
        public ActorStats m_BaseStats;
        public float m_Damage;
    }

    [Serializable]
    public struct PlayerStats
    {
        public ActorStats m_BaseStats;
        
        public int m_MaxCardsInHand;
    }
}
