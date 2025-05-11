using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.JobSystem;
using Project.Wrappers;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Project.Game.Battle.Controllers
{
    public class EnemiesInBattleController: MonoBehaviour{
        
        [Inject]
        private void Construct(SignalBus signalBus){
            m_SignalBus = signalBus;
        }
        private SignalBus m_SignalBus;
        private List<EnemyView> m_EnemiesInBattle = new();
        
        private List<Func<bool>> m_EnemyDieValidators = new();
        
        private Dictionary<EnemyView, Coroutine> m_ActiveEnemyDyingRoutines = new();
    
        void OnEnable()
        {
            m_SignalBus.Subscribe<BattleStartSignal>(OnBattleStartInteraction);
        }

        void OnDisable()
        {
            m_SignalBus.Unsubscribe<BattleStartSignal>(OnBattleStartInteraction);
            
            UnsubscribeForEnemyDamageTaken();
        }

        public void AddEnemyDieValidator(Func<bool> awaiter) =>
            m_EnemyDieValidators.Add(awaiter);
        public void RemoveEnemyDieValidator(Func<bool> awaiter) =>
            m_EnemyDieValidators.Remove(awaiter);


        private IEnumerator OnBattleStartInteraction(BattleStartSignal signal)
        {
            m_EnemiesInBattle = signal.GetEnemiesInBattle();
            
            SubscribeForEnemyDamageTaken();
            
            EnableEnemiesIdleAnimations();
            
            yield return null;
        }
        
        private void OnEnemyDamageTaken(EnemyView enemy){            
            if(m_ActiveEnemyDyingRoutines.ContainsKey(enemy)){return;}
            var die_routine = StartCoroutine(OnEnemyDamageTakenRoutine(enemy));
            
            m_ActiveEnemyDyingRoutines.Add(enemy, die_routine);
        }

        private IEnumerator OnEnemyDamageTakenRoutine(EnemyView enemy){
            m_SignalBus.SendSignal(new EnemyHealthChangedSignal(enemy));

            if (enemy.GetController().GetCurrentHealth() == 0)
            {
                m_EnemiesInBattle.Remove(enemy);
                yield return new JobSwitchColliderEnabledState(enemy.gameObject, false).Proccess();

                while (m_EnemyDieValidators.Any(v => !v.Invoke())){
                    yield return null;
                }
                
                m_SignalBus.SendSignal(new EnemyDiedSignal(enemy));
                                
                yield return EnemyDieSequence(enemy);
                
                m_ActiveEnemyDyingRoutines.Remove(enemy);
            }
        }
        
        private IEnumerator EnemyDieSequence(EnemyView enemy){
            enemy.StopIdleAnimation();
                    
            yield return enemy.GetController().GetDieAnimation();
            
            Destroy(enemy.gameObject);
        }
        
        
        private void SubscribeForEnemyDamageTaken(){
            foreach (var e in m_EnemiesInBattle)
            {
                e.GetController().OnDamageTaken += OnEnemyDamageTaken;
            }
        }
        private void UnsubscribeForEnemyDamageTaken(){
            foreach (var e in m_EnemiesInBattle)
            {
                e.GetController().OnDamageTaken -= OnEnemyDamageTaken;
            }
        }
    
        private void EnableEnemiesIdleAnimations(){
            foreach(var e in m_EnemiesInBattle){
                e.StartIdleAnimation();
            }
        }
        private void DisableEnemiesIdleAnimations(){
            foreach(var e in m_EnemiesInBattle){
                e.StopIdleAnimation();
            }
        }
    }
}
