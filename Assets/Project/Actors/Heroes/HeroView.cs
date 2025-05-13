using System;
using UnityEngine;

namespace Project.Actors{
    public class HeroView: MonoBehaviour{
        public void Init(HeroState heroState)
        {
            m_State = heroState;
        }
        private HeroState m_State;
        
        public HeroState GetState() => m_State;
        
        public int GetInitiative() => m_State.m_stats.m_BaseStats.m_Initiative;
        
        public event Action<HeroView> OnDamageTaken;
        
        public void TakeDamage(float amount){
            
            m_State.TakeDamage(amount);
            
            OnDamageTaken?.Invoke(this);
        }

    }
    
}
