using System;
using System.Collections;
using System.Collections.Generic;
using Project.TurnSystem;
using UnityEngine;

namespace Project.Player
{

    public class PlayerData
    {
        public event Action<PlayerData> OnDamageTaken;
        
        private float m_Initiative = 10f;
        public float GetInitiaive() => m_Initiative;
        
        private float m_Health = 100.0f;
        private float m_MaxHealth = 100.0f;
        public float GetMaxHealth() => m_MaxHealth;
        public float GetCurrentHealth() => m_Health;
        
        public void TakeDamage(float amount){
            m_Health = Mathf.Clamp(m_Health - amount, 0, m_MaxHealth);
            OnDamageTaken?.Invoke(this);
        }
    }
}
