using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.JobSystem;
using Project.Wrappers;
using UnityEngine;
using XL1TTE.Animator;
using Zenject;

namespace Project.Game.Battle.Controllers
{
    public class EnemiesInBattleController: MonoBehaviour{
        
        [Inject]
        private void Construct(SignalBus signalBus){
            m_SignalBus = signalBus;
        }
        private SignalBus m_SignalBus;
        
        
        private IReadOnlyList<EnemyView> m_Enemies;
            
        private List<Func<bool>> m_EnemyDieValidators = new();
        
        private Dictionary<EnemyView, AwaitableCoroutine> m_ActiveEnemyDieRoutines = new();
                    
        void OnEnable()
        {
            m_SignalBus.Subscribe<BattleStageReadySignal>(BattleStageProccess);
        }

        void OnDisable()
        {
            m_SignalBus.Unsubscribe<BattleStageReadySignal>(BattleStageProccess);
        }

        void OnDestroy()
        {
            m_ActiveEnemyDieRoutines = null;
            m_EnemyDieValidators = null;
            
            FreePreviousBattleStage();
        }

        public void AddEnemyDieValidator(Func<bool> awaiter) =>
            m_EnemyDieValidators.Add(awaiter);
        public void RemoveEnemyDieValidator(Func<bool> awaiter) =>
            m_EnemyDieValidators.Remove(awaiter);


        private void BattleStageProccess(BattleStageReadySignal signal)
        {
            FreePreviousBattleStage();

            m_Enemies = signal.Stage.GetEnemies();
            
            foreach(var enemy in m_Enemies){
                enemy.OnHealthChanged += OnEnemyDamageTaken;
                enemy.StartIdleAnimation();
            }
        }
        private void FreePreviousBattleStage(){
            if(m_Enemies == null) {return; }
            
            foreach(var enemy in m_Enemies){
                enemy.OnHealthChanged -= OnEnemyDamageTaken;
            }
        }
        
        private void OnEnemyDamageTaken(EnemyView enemy){            
            StartCoroutine(OnEnemyDamageTakenRoutine(enemy));
        }
        
        
        private IEnumerator OnEnemyDamageTakenRoutine(EnemyView enemy)
        {
            yield return NotifyEnemyHealthChanged(enemy);

            if(isEnemyJustDied(enemy)){

                AwaitableCoroutine dieRoutine = new AwaitableCoroutine(this, EnemyDyingRoutine(enemy));
                
                m_ActiveEnemyDieRoutines.Add(enemy, dieRoutine);
                
                while(!dieRoutine.IsDone){
                    yield return null;
                }
                
                m_ActiveEnemyDieRoutines.Remove(enemy);
                
                NotifyEnemyDied(enemy);
            }
        }
        private IEnumerator NotifyEnemyHealthChanged(EnemyView enemy)
        {
            m_SignalBus.SendSignal(new EnemyHealthChangedSignal(enemy));
            yield return null;
        } 
        private IEnumerator EnemyDyingRoutine(EnemyView enemy){

            yield return new JobSwitchColliderEnabledState(enemy.gameObject, false).Proccess();

            while (m_EnemyDieValidators.Any(v => !v.Invoke())){
                yield return null;
            }
            
            yield return PlayEnemyDieAnimation(enemy);
        }
        private IEnumerator PlayEnemyDieAnimation(EnemyView enemy)
        {
            enemy.StopIdleAnimation();

            yield return enemy.m_dieAnimation.Play().WaitForCompletion();
        }

        private bool isEnemyJustDied(EnemyView enemy) =>
            enemy.GetState().GetCurrentHealth() == 0
                && !m_ActiveEnemyDieRoutines.ContainsKey(enemy);
        public bool isAnyEnemyDying() =>
            !m_ActiveEnemyDieRoutines.IsEmpty();
        private void NotifyEnemyDied(EnemyView enemy){
            m_SignalBus.SendSignal(new EnemyDiedSignal(enemy));
        }

    }
}
