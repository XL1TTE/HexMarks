using System.Collections.Generic;
using System.Runtime.InteropServices;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.GameManagers{
    
    public class HealthBarManager: MonoBehaviour{
        
        [Inject]
        private void Construct(SignalBus signalBus, IHealthBarFactory factory){
            m_SignalBus = signalBus;
            m_Factory = factory;
            
            Enable();
        }
        
        private SignalBus m_SignalBus;
        private IHealthBarFactory m_Factory;
        private Dictionary<Enemy, HealthBar> m_Cache = new();
        
        public void Disable(){
            m_SignalBus.Unsubscribe<EnemySpawnedSignal>(OnEnemySpawned);
            m_SignalBus.Unsubscribe<EnemyDiedSignal>(OnEnemyDied);
            m_SignalBus.Unsubscribe<EnemyHealthChangedSignal>(OnEnemyHealthChanged);
        } 
        public void Enable(){
            m_SignalBus.Subscribe<EnemySpawnedSignal>(OnEnemySpawned);
            m_SignalBus.Subscribe<EnemyDiedSignal>(OnEnemyDied);
            m_SignalBus.Subscribe<EnemyHealthChangedSignal>(OnEnemyHealthChanged);
        } 
        
        
        private void OnEnemyHealthChanged(EnemyHealthChangedSignal signal)
        {
            var enemy = signal.GetEnemy();

            if (!m_Cache.TryGetValue(enemy, out var bar)) { return; }

            bar.UpdateProgress(enemy.GetCurrentHealth() / enemy.GetMaxHealth());
        }
        private void OnEnemySpawned(EnemySpawnedSignal signal)
        {
            var enemy = signal.GetEnemy();
            
            HealthBar bar = m_Factory.CreateHealthBar(enemy.transform);
            bar.UpdateProgress(enemy.GetCurrentHealth() / enemy.GetMaxHealth());

            m_Cache.Add(enemy, bar);
        }
        private void OnEnemyDied(EnemyDiedSignal signal){
            var enemy = signal.GetEnemy();
            if(!m_Cache.TryGetValue(enemy, out var bar)){return; }
            
            m_Factory.ReturnToPool(bar);
            
            m_Cache.Remove(enemy);
        }
        
    }
}
