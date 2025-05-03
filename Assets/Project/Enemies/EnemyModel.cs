using System.Collections;
using Project.Enemies.AIs;
using Project.TurnSystem;
using UnityEngine;

namespace Project.Enemies{
    public class EnemyModel
    {
        
        public EnemyModel(float health, float maxHealth, float damage, int initiative){
            m_Health = health;
            m_MaxHealth = maxHealth;
            m_Damage = damage;
            m_Initiaive = initiative;


            m_AI = new EnemyAI(this);
        }
        
        private int m_Initiaive;
        private float m_Health;
        private float m_MaxHealth;
        private float m_Damage;
        
        private EnemyAI m_AI;
        
        private BaseEnemyDieAnimation m_DieAnimation;
        public void SetDieAnimation(BaseEnemyDieAnimation anim) => m_DieAnimation = anim;
        
        public IEnumerator GetDieSequence(EnemyView view) => m_DieAnimation.GetDieSequence(view);

        public void TakeDamage(float amount){
            m_Health = Mathf.Clamp(m_Health - amount, 0, m_MaxHealth);
        }
        public float GetMaxHealth() => m_MaxHealth;
        public float GetCurrentHealth() => m_Health;
        public float GetEnemyDamage() => m_Damage;
        
        public int GetInitiaive() => m_Initiaive;
        
        public EnemyAI GetAI() => m_AI;
        
    }
    
}
