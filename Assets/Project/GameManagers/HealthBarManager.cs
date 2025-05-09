using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.UI;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Project.GameManagers{
    
    public class HealthBarManager: MonoBehaviour{
        
        [Inject]
        private void Construct(SignalBus signalBus, IHealthBarFactory factory){
            m_SignalBus = signalBus;
            m_Factory = factory;
        }
        
        private SignalBus m_SignalBus;
        private IHealthBarFactory m_Factory;
        private Dictionary<EnemyView, HealthBar> m_Cache = new();
        
        public void OnDisable(){
            m_SignalBus.Unsubscribe<EnemySpawnedSignal>(OnEnemySpawned);
            m_SignalBus.Unsubscribe<EnemyDiedSignal>(OnEnemyDied);
            m_SignalBus.Unsubscribe<EnemyHealthChangedSignal>(OnEnemyHealthChanged);
        } 
        public void OnEnable(){
            m_SignalBus.Subscribe<EnemySpawnedSignal>(OnEnemySpawned);
            m_SignalBus.Subscribe<EnemyDiedSignal>(OnEnemyDied);
            m_SignalBus.Subscribe<EnemyHealthChangedSignal>(OnEnemyHealthChanged);
        } 
        
        
        private IEnumerator OnEnemyHealthChanged(EnemyHealthChangedSignal signal)
        {
            var enemyView = signal.GetEnemy();
            var enemy = enemyView.GetController();

            if (!m_Cache.TryGetValue(enemyView, out var bar)) { yield break; }

            bar.UpdateProgress(enemy.GetCurrentHealth() / enemy.GetMaxHealth());
        }
        private IEnumerator OnEnemySpawned(EnemySpawnedSignal signal)
        {
            var enemyView = signal.GetEnemy();
            var enemy = enemyView.GetController();

            HealthBar bar = m_Factory.CreateHealthBar(enemyView.transform);
            bar.UpdateProgress(enemy.GetCurrentHealth() / enemy.GetMaxHealth());

            m_Cache.Add(enemyView, bar);
            yield return null;
        }
        private IEnumerator OnEnemyDied(EnemyDiedSignal signal){
            var enemy = signal.GetEnemy();
            if(!m_Cache.TryGetValue(enemy, out var bar)){yield break; }
            
            m_Factory.ReturnToPool(bar);
            
            m_Cache.Remove(enemy);
        }
        
    }
}
