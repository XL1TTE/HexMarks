using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Project.Actors;
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
        private Dictionary<EnemyView, HealthBar> m_EnemiesCache = new();
        private Dictionary<HeroView, HealthBar> m_HeroesCache = new();
        
        public void OnDisable(){
            m_SignalBus.Unsubscribe<EnemySpawnedSignal>(OnEnemySpawned);
            m_SignalBus.Unsubscribe<HeroSpawnedSignal>(OnHeroSpawned);

            m_SignalBus.Unsubscribe<EnemyDiedSignal>(OnEnemyDied);
            m_SignalBus.Unsubscribe<HeroDiedSignal>(OnHeroDied);
            m_SignalBus.Unsubscribe<EnemyHealthChangedSignal>(OnEnemyHealthChanged);

            DisableEnemiesHealthBars();
            DisableHeroesHealthBars();
        } 
        public void OnEnable(){
            m_SignalBus.Subscribe<EnemySpawnedSignal>(OnEnemySpawned);
            m_SignalBus.Subscribe<HeroSpawnedSignal>(OnHeroSpawned);
            
            m_SignalBus.Subscribe<EnemyDiedSignal>(OnEnemyDied);
            m_SignalBus.Subscribe<HeroDiedSignal>(OnHeroDied);
            m_SignalBus.Subscribe<EnemyHealthChangedSignal>(OnEnemyHealthChanged);
        }


        private void DisableEnemiesHealthBars(){
            foreach(var bar in m_EnemiesCache.Values){
                if(bar != null){
                    m_Factory.ReturnEnemyBarToPool(bar);
                }
            }
            m_EnemiesCache.Clear();
        }
        private void DisableHeroesHealthBars(){
            foreach (var bar in m_HeroesCache.Values)
            {
                if (bar != null)
                {
                    m_Factory.ReturnHeroBarToPool(bar);
                }
            }
            m_HeroesCache.Clear();
        }
        
        private IEnumerator OnEnemyHealthChanged(EnemyHealthChangedSignal signal)
        {
            var enemyView = signal.GetEnemy();
            var enemy = enemyView.GetController();

            if (!m_EnemiesCache.TryGetValue(enemyView, out var bar)) { yield break; }

            bar.UpdateProgress(enemy.GetCurrentHealth() / enemy.GetMaxHealth());
        }
        private IEnumerator OnEnemySpawned(EnemySpawnedSignal signal)
        {
            var enemyView = signal.GetEnemy();
            var enemy = enemyView.GetController();

            HealthBar bar = m_Factory.CreateEnemyHealthBar(enemyView.transform);
            bar.UpdateProgress(enemy.GetCurrentHealth() / enemy.GetMaxHealth());

            m_EnemiesCache.Add(enemyView, bar);
            yield return null;
        }
        private IEnumerator OnEnemyDied(EnemyDiedSignal signal){
            var enemy = signal.GetEnemy();
            if(!m_EnemiesCache.TryGetValue(enemy, out var bar)){yield break; }
            
            m_Factory.ReturnEnemyBarToPool(bar);
            
            m_EnemiesCache.Remove(enemy);
        }
        
        private IEnumerator OnHeroSpawned(HeroSpawnedSignal signal){
            var heroView = signal.GetHero();
            var heroState = heroView.GetState();
            
            HealthBar bar = m_Factory.CreateHeroHealthBar(heroView.transform, IHealthBarFactory.BarAlignment.BUTTOM);
            bar.UpdateProgress(heroState.GetCurrentHealth() / heroState.GetMaxHealth());

            m_HeroesCache.Add(heroView, bar);
            
            yield return null;
        }
        
        private IEnumerator OnHeroDied(HeroDiedSignal signal){
            var hero = signal.GetHero();
            if (!m_HeroesCache.TryGetValue(hero, out var bar)) { yield break; }
            
            m_Factory.ReturnHeroBarToPool(bar);
            
            m_HeroesCache.Remove(hero);

        }

    }
}
