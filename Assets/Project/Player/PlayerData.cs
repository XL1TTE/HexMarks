using System;
using System.Collections;
using System.Collections.Generic;
using Project.Actors.Stats;
using Project.TurnSystem;
using UnityEngine;

namespace Project.Player
{
    
    public class PlayerInBattle : IHealthStatsAccessor
    {
        public PlayerInBattle(PlayerStats stats)
        {
            m_Stats = stats;
        }
        
        private PlayerStats m_Stats;
        
        public float GetMaxHealth() => m_Stats.m_BaseStats.m_MaxHealth;
        public float GetCurrentHealth() => m_Stats.m_BaseStats.m_Health;
        public int GetInitiative() => m_Stats.m_BaseStats.m_Initiative;
        public int GetHandCapacity() => m_Stats.m_MaxCardsInHand;
        public void SetHandCapacity(int value){
            if(value < 0 ){
                throw new Exception("Hand capacity cannot be negative!");
            }
            m_Stats.m_MaxCardsInHand = value;
        } 

        public event Action<PlayerInBattle> OnDamageTaken;

        public void TakeDamage(float amount)
        {
            m_Stats.m_BaseStats.m_Health = Mathf.Clamp(m_Stats.m_BaseStats.m_Health - amount, 0, m_Stats.m_BaseStats.m_MaxHealth);
            OnDamageTaken?.Invoke(this);
        }
    }
    
    [Serializable]
    public class PlayerData
    {
        public PlayerData() {}
        [SerializeField] private PlayerStats m_Stats;
        
        public PlayerInBattle GetPlayerInBattle() 
            => new PlayerInBattle(m_Stats);
    }
}
