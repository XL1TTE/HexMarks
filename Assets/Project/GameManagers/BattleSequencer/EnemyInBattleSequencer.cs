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
            m_SignalBus.Subscribe<CardUsedSignal>(OnCardPlayed);
        }
        
        ~EnemyInBattleSequencer(){
            m_SignalBus.Unsubscribe<EnemySpawnedSignal>(OnEnemySpawned);
            m_SignalBus.Unsubscribe<CardUsedSignal>(OnCardPlayed);
        }

        private SignalBus m_SignalBus;
        private BattleSequenceManager m_manager;

        private List<AwaitedCoroutine> m_CardEffectAwaiters = new();
        private Dictionary<EnemyView, AwaitedCoroutine> m_EnemyDieAwaiter = new();
        
        public bool isAlowedToProccessTurn = true;

        private void OnEnemySpawned(EnemySpawnedSignal signal)
        {
            var enemy = signal.GetEnemy();
            
            enemy.StartIdleAnimation();

            enemy.GetController().OnDamageTaken += OnEnemyDamageTaken;
        }

        private void OnEnemyDamageTaken(EnemyView view)
        {
            m_SignalBus.SendSignal(new EnemyHealthChangedSignal(view));

            if (view.GetController().GetCurrentHealth() == 0)
            {
                AddEnemyDieAwaiter(view, new AwaitedCoroutine(m_manager, EnemyDeathRoutine(view)));
            }
        }

        private IEnumerator EnemyDeathRoutine(EnemyView enemy)
        {
            isAlowedToProccessTurn = false;
            yield return AwaitCardEffects();

            m_SignalBus.SendSignal(new EnemyDiedSignal(enemy));

            enemy.StopIdleAnimation();

            yield return PlayEnemyDieSequence(enemy);

            isAlowedToProccessTurn = true;
        }

        private IEnumerator PlayEnemyDieSequence(EnemyView enemy){
            yield return new JobSwitchColliderEnabledState(enemy.gameObject, false).Proccess();
            
            var enemyDieSequence = enemy.GetController().GetDieAnimation();
            yield return enemyDieSequence;

            yield return new JobSwitchColliderEnabledState(enemy.gameObject, true).Proccess();

            Object.Destroy(enemy.gameObject);
        }

        private void OnCardPlayed(CardUsedSignal signal)
        {
            var card = signal.GetCardView();

            // Launch card use sequence and adds it to awaiters of enemy
            var cardUsingAwaiter = new AwaitedCoroutine(m_manager, card.GetCardUseSequence());

            AddCardEffectAwaiter(cardUsingAwaiter);
        }


        public IEnumerator AwaitCardEffects()
        {        
            while (m_CardEffectAwaiters.Any(w => !w.IsDone))
            {
                yield return null;
            }

            m_CardEffectAwaiters.Clear();
        }
        public IEnumerator AwaitEnemyDie(EnemyView enemy)
        {
            if (!m_EnemyDieAwaiter.TryGetValue(enemy, out var awaiter))
            {
                yield break;
            }

            while (!awaiter.IsDone)
            {
                yield return null;
            }

            m_EnemyDieAwaiter.Remove(enemy);
        }

        private void AddCardEffectAwaiter(AwaitedCoroutine routine)
        {
            m_CardEffectAwaiters.Add(routine);
        }
        private void AddEnemyDieAwaiter(EnemyView enemy, AwaitedCoroutine routine)
        {
            if (m_EnemyDieAwaiter.TryGetValue(enemy, out var awaiters))
            {
                return;
            }
            else
            {
                m_EnemyDieAwaiter.Add(enemy, routine);
            }
        }
    }
}
