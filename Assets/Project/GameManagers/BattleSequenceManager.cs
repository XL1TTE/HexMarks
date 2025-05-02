using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Cards;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.JobSystem;
using Project.Wrappers;
using UnityEngine;
using Zenject;

namespace Project.GameManagers{
    public class BattleSequenceManager: MonoBehaviour{
        
        [Inject]
        private void Construct(SignalBus signalBus){
            m_SignalBus = signalBus;
            
            m_SignalBus.Subscribe<EnemyDyingSignal>(OnEnemyDeath);
            m_SignalBus.Subscribe<CardUsedOnEnemySignal>(OnCardPlayedOnEnemy);
        }
        
        private SignalBus m_SignalBus;
        
        private Dictionary<Enemy, List<AwaitedCoroutine>> m_EnemiesCoroutines = new();
        
        private void OnEnemyDeath(EnemyDyingSignal signal)
        {
            var enemy = signal.GetEnemy();
            StartCoroutine(StartEnemyDeathAfterCoroutines(enemy));
        }
        
        private IEnumerator StartEnemyDeathAfterCoroutines(Enemy enemy){
            if(!m_EnemiesCoroutines.TryGetValue(enemy, out var coroutines)){enemy.Die();}
            
            while (coroutines.Any(w => !w.IsDone))
            {
                yield return null;
            }
            
            m_EnemiesCoroutines.Remove(enemy);
            enemy.Die();
        }
        
        private void OnCardPlayedOnEnemy(CardUsedOnEnemySignal signal){
            var card = signal.GetCardView();
            var enemy = signal.GetTarget();

            var CardUsingCoroutine = new AwaitedCoroutine(this, card.GetUseCardRoutine());


            if (m_EnemiesCoroutines.TryGetValue(enemy, out var coroutines)){
               coroutines.Add(CardUsingCoroutine); 
            }
            else{
                m_EnemiesCoroutines.Add(enemy, new List<AwaitedCoroutine> {CardUsingCoroutine});
            }
        }

    }
}
