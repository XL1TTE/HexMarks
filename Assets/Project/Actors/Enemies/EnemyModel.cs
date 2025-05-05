using System.Collections;
using Project.Actors.Stats;
using Project.Enemies.AIs;
using Project.TurnSystem;
using UnityEngine;

namespace Project.Enemies{
    public class EnemyModel
    {
        
        public EnemyModel(EnemyStats stats)
        {
            m_Stats = stats;

            m_AI = new EnemyAI(this);
        }
        
        private EnemyStats m_Stats;
        
        private EnemyAI m_AI;
        
        private BaseEnemyDieAnimation m_DieAnimation;
        public void SetDieAnimation(BaseEnemyDieAnimation anim) => m_DieAnimation = anim;
        
        public IEnumerator GetDieSequence(EnemyView view) => m_DieAnimation.GetDieSequence(view);

        public void TakeDamage(float amount){
            m_Stats.m_BaseStats.m_Health = Mathf.Clamp(m_Stats.m_BaseStats.m_Health - amount, 0, m_Stats.m_BaseStats.m_MaxHealth);
        }
        public float GetMaxHealth() => m_Stats.m_BaseStats.m_MaxHealth;
        public float GetCurrentHealth() => m_Stats.m_BaseStats.m_Health;
        public float GetEnemyDamage() => m_Stats.m_Damage;
        
        public int GetInitiaive() => m_Stats.m_BaseStats.m_Initiative;
        
        public EnemyAI GetAI() => m_AI;
        
    }
    
}
