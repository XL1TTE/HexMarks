using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.JobSystem;
using Project.TurnSystem;
using Project.Utilities.Extantions;
using Project.Wrappers;
using UnityEngine;

namespace Project.GameManagers.BattleSequence{
    public class EnemyInBattleSequencer{
        
        public EnemyInBattleSequencer(SignalBus signalBus, BattleSequenceManager manager){
            m_SignalBus = signalBus;
            m_manager = manager;

            m_SignalBus.Subscribe<EnemySpawnedSignal>(OnEnemySpawned);
            m_SignalBus.Subscribe<CardUsedOnEnemySignal>(OnCardPlayedOnEnemy);
        }
        
        ~EnemyInBattleSequencer(){
            m_SignalBus.Unsubscribe<EnemySpawnedSignal>(OnEnemySpawned);
            m_SignalBus.Unsubscribe<CardUsedOnEnemySignal>(OnCardPlayedOnEnemy);
        }

        private SignalBus m_SignalBus;
        private BattleSequenceManager m_manager;

        private Dictionary<EnemyView, List<AwaitedCoroutine>> m_enemiesRoutinesToAwait = new();
        
        public bool isAlowedToProccessTurn = true;

        private void OnEnemySpawned(EnemySpawnedSignal signal)
        {
            var enemy = signal.GetEnemy();

            enemy.GetController().OnDamageTaken += OnEnemyDamageTaken;
        }

        private void OnEnemyDamageTaken(EnemyView view)
        {
            m_SignalBus.SendSignal(new EnemyHealthChangedSignal(view));

            if (view.GetController().GetCurrentHealth() == 0)
            {
                m_manager.StartCoroutine(EnemyDeathRoutine(view));
            }
        }

        private IEnumerator EnemyDeathRoutine(EnemyView enemy)
        {
            isAlowedToProccessTurn = false;
            yield return WaitForAllRoutinesOnEnemy(enemy);
            isAlowedToProccessTurn = true;

            yield return PlayEnemyDieSequence(enemy);
            
            m_SignalBus.SendSignal(new EnemyDiedSignal(enemy));

            Object.Destroy(enemy.gameObject);
        }
        
        private IEnumerator WaitForAllRoutinesOnEnemy(EnemyView enemy)
        {
            
            
            if (!m_enemiesRoutinesToAwait.TryGetValue(enemy, out var awaiters))
            {
                yield break;
            }

            while (awaiters.Any(w => !w.IsDone))
            {
                yield return null;
            }

            m_enemiesRoutinesToAwait.Remove(enemy);
        }

        private IEnumerator PlayEnemyDieSequence(EnemyView enemy){
            yield return new JobSwitchColliderEnabledState(enemy.gameObject, false).Proccess();
            
            var enemyDieSequence = enemy.GetController().GetDieSequence();
            yield return enemyDieSequence;

            yield return new JobSwitchColliderEnabledState(enemy.gameObject, true).Proccess();
        }

        private void OnCardPlayedOnEnemy(CardUsedOnEnemySignal signal)
        {
            var card = signal.GetCardView();
            var enemy = signal.GetTarget();

            // Launch card use sequence and adds it to awaiters of enemy
            var cardUsingAwaiter = new AwaitedCoroutine(m_manager, card.GetCardUseSequence());


            if (m_enemiesRoutinesToAwait.TryGetValue(enemy, out var awaiters))
            {
                awaiters.Add(cardUsingAwaiter);
            }
            else
            {
                m_enemiesRoutinesToAwait.Add(enemy, new List<AwaitedCoroutine> { cardUsingAwaiter });
            }
        }
    }
}
