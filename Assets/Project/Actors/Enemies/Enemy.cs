using System;
using System.Collections;
using Project.Enemies.AIs;

namespace Project.Enemies{
    
    public class Enemy{
        
        public Enemy(EnemyView enemyView, EnemyModel enemyModel){
            m_View = enemyView;
            m_Model = enemyModel;
            m_View.Init(this);
        }
        
        private EnemyView m_View;
        private EnemyModel m_Model;
        
        public event Action<EnemyView> OnDamageTaken;
        
        public void TakeDamage(float amount){
            m_Model.TakeDamage(amount);
            OnDamageTaken?.Invoke(m_View);
        }
        
        public float GetMaxHealth() => m_Model.GetMaxHealth();
        public float GetCurrentHealth() => m_Model.GetCurrentHealth();
        public float GetEnemyDamage() => m_Model.GetEnemyDamage();
        
        public EnemyAI GetAI() => m_Model.GetAI();
        
        public int GetInitiaive() => m_Model.GetInitiaive();
        
        public IEnumerator GetDieSequence() => m_Model.GetDieSequence(m_View);
    }
    
}
