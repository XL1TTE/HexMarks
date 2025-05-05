
using System;

namespace Project.Actors.Stats{
    
    
    public interface IHealthStatsAccessor{
        float GetCurrentHealth();
        float GetMaxHealth();
    }
    
    [Serializable]
    public struct ActorStats
    {
        public ActorStats(float health, float maxHealth, int initiative){
            m_Health = health;
            m_MaxHealth = maxHealth;
            m_Initiative = initiative;          
        }
        
        public float m_Health;
        public float m_MaxHealth;        
        public int m_Initiative;
    }
    
    [Serializable]
    public struct EnemyStats
    {
        public EnemyStats(ActorStats baseStats, float damage)
        {
            m_Damage = damage;
            m_BaseStats = baseStats;
        }
        
        public ActorStats m_BaseStats;
        public float m_Damage;
    }

    [Serializable]
    public struct PlayerStats
    {
        public PlayerStats(ActorStats baseStats){
            m_BaseStats = baseStats;
        }
        public ActorStats m_BaseStats;
    }
}
