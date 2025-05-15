using System;
using System.Collections;
using System.Collections.Generic;
using CMSystem;
using Project.Data.CMS.Tags.Enemies;
using XL1TTE.Animator;

namespace Project.Enemies{
    
    public class Enemy{
        
        public Enemy(EnemyView enemyView, EnemyState enemyModel){
            m_View = enemyView;
            m_State = enemyModel;
            m_View.Init(this);
        }
        
        private EnemyView m_View;
        private EnemyState m_State;
        
        public event Action<EnemyView> OnDamageTaken;
        
        public void TakeDamage(float amount){
            m_State.TakeDamage(amount);
            OnDamageTaken?.Invoke(m_View);
        }
        
        public float GetMaxHealth() => m_State.GetMaxHealth();
        public float GetCurrentHealth() => m_State.GetCurrentHealth();
        public float GetEnemyDamage() => m_State.GetEnemyDamage();
        
        public CMSEntity GetModel() => m_State.GetModel();
        
        public int GetInitiaive() => m_State.GetInitiaive();
        
        public xlAnimation GetDieAnimation() => m_State.GetDieAnimation(m_View);
        public xlAnimation GetIdleAnimation() => m_State.GetIdleAnimation(m_View);
        public xlAnimation GetAttackAnimation() => m_State.GetAttackAnimation(m_View);
    }
    
}
