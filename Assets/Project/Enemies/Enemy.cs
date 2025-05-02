using Project.EventBus;
using Project.EventBus.Signals;
using UnityEngine;
using Zenject;

namespace Project.Enemies
{
    public class Enemy : MonoBehaviour{
        
        [Inject]
        private void Construct(SignalBus signalBus){
            m_SignalBus = signalBus;
        } 
        
        private SignalBus m_SignalBus;
        
        private float m_Health = 100.0f;
        public float GetCurrentHealth() => m_Health;
        private float m_MaxHealth = 100.0f;
        public float GetMaxHealth() => m_MaxHealth;
        
        
        public void TakeDamage(float amount){
            m_Health = Mathf.Clamp(m_Health - amount, 0, m_MaxHealth);
            m_SignalBus.SendSignal(new EnemyHealthChangedSignal(this));
            
            if(IsDied()){ OnDeathNotify();}
        }
        public void Heal(float amount){
            m_Health = Mathf.Clamp(m_Health + amount, 0, m_MaxHealth);
            m_SignalBus.SendSignal(new EnemyHealthChangedSignal(this));
        }  
        private bool IsDied(){
            if(m_Health <= 0){
                return true;
            }
            return false;
        }
        
        private void OnDeathNotify(){
            m_SignalBus.SendSignal(new EnemyDyingSignal(this));
        }
        
        public void Die(){
            m_SignalBus.SendSignal(new EnemyDiedSignal(this));
            Destroy(gameObject);
        }
    }
}
