using System.Collections;
using System.Collections.Generic;
using Project.Cards;
using Project.Data.CMS.Tags.Enemies;
using Project.DataResolving;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.Game.Battle.UI;
using Project.GameManagers.BattleSequence;
using Project.JobSystem;
using Project.Layouts;
using Project.Player;
using Project.TurnSystem;
using Project.Utilities.Extantions;
using UnityEngine;
using Zenject;

namespace Project.GameManagers{
    public class BattleSequenceManager: MonoBehaviour{


        [Inject]
        private void Construct(SignalBus signalBus, DataResolver resolver)
        {
            m_SignalBus = signalBus;
            m_DataResolver = resolver;
        }

        [SerializeField] private BattleUI m_BattleUI; 
        
        private PlayerInBattle m_PlayerInBattle;
        private SignalBus m_SignalBus;
        private DataResolver m_DataResolver;
        private ICardFactory m_CardFactory;

        private List<EnemyView> m_EnemiesInBattle = new();


        #region BattleSequencers

        private EnemyInBattleSequencer m_EnemySequencer;

        #endregion

        private void OnBattleStarted(BattleStartSignal signal)
        {
            StartCoroutine(BattleStartRoutine(signal));
        }
        private IEnumerator BattleStartRoutine(BattleStartSignal signal)
        {
            m_EnemiesInBattle = signal.GetEnemiesInBattle();
            m_PlayerInBattle = signal.GetPlayerInBattle();

            // Initial notification
            m_SignalBus.SendSignal(new PlayerHealthChangedSingal(m_PlayerInBattle));
            
            yield return new WaitForSeconds(2f);
        }
        
        private IEnumerator OnPlayerDamageTakeRoutine(PlayerInBattle player){

            m_SignalBus.SendSignal(new PlayerHealthChangedSingal(player));

            if (player.GetCurrentHealth() == 0)
            {
                m_SignalBus.SendSignal(new PlayerLostBattleSignal());
            }
            yield return null;
        }
        
        private void OnEnemyDied(EnemyDiedSignal signal){
            StartCoroutine(OnEnemyDiedRoutine(signal));
        }
        private IEnumerator OnEnemyDiedRoutine(EnemyDiedSignal signal){
            var enemy = signal.GetEnemy();

            var enemyModel = enemy.GetController().GetModel();
            if (enemyModel.Is<TagOnDieActions>(out var onDie))
            {
                foreach (var a in onDie.actions)
                {
                    var context = m_DataResolver.Resolve(a);
                    yield return a.GetAction(enemy, context);
                }
            }

            yield return m_EnemySequencer.AwaitEnemyDie(enemy);
            
            yield return new WaitForSeconds(0.5f);
        }
    }
}
