using System;
using System.Collections;
using System.Collections.Generic;
using Project.Data.CMS.Tags.Enemies;
using Project.DataResolving;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.JobSystem;
using Project.TurnSystem;
using Project.Utilities.Extantions;
using UnityEngine;
using Zenject;

namespace Project.Game.Battle.Controllers
{
    public class BattleTurnsController: MonoBehaviour{
        
        [Inject]
        private void Construct(SignalBus signalBus, DataResolver dataResolver){
            m_SignalBus = signalBus;
            m_DataResolver = dataResolver;
        }
        void OnEnable()
        {
            m_SignalBus.Subscribe<BattleStartSignal>(OnBattleStartInteraction);
            m_SignalBus.Subscribe<PlayerTurnSignal>(OnPlayerTurnInteraction);
            m_SignalBus.Subscribe<EnemyTurnSignal>(OnEnemyTurnInteraction);
            
            m_SignalBus.Subscribe<EnemyDiedSignal>(OnEnemyDiedInteraction);
        }

        void OnDisable()
        {
            m_SignalBus.Unsubscribe<BattleStartSignal>(OnBattleStartInteraction);
            m_SignalBus.Unsubscribe<PlayerTurnSignal>(OnPlayerTurnInteraction);
            m_SignalBus.Unsubscribe<EnemyTurnSignal>(OnEnemyTurnInteraction);

            m_SignalBus.Unsubscribe<EnemyDiedSignal>(OnEnemyDiedInteraction);
        }

        private SignalBus m_SignalBus;
        private DataResolver m_DataResolver;


        private List<ITurnTaker> m_TurnsQueue;

        private IEnumerator OnPlayerTurnInteraction(PlayerTurnSignal signal)
        {
            yield return null;
        }

        private IEnumerator OnEnemyTurnInteraction(EnemyTurnSignal signal)
        {
            var enemy = signal.GetEnemy();
            var enemyModel = enemy.GetController().GetModel();

            if (enemyModel.Is<TagOnTurnActions>(out var onTurn))
            {
                foreach (var a in onTurn.actions)
                {
                    var context = m_DataResolver.Resolve(a);
                    yield return a.GetAction(enemy, context);
                }
            }

            yield return ProccessNextTurn();
        }

        private IEnumerator OnEnemyDiedInteraction(EnemyDiedSignal signal){
            var enemy = signal.GetEnemy();

            m_TurnsQueue.TryRemove((t) => t is EnemyTurnTaker tt && tt.GetEnemy() == enemy);
            yield return null;
        }

        private IEnumerator OnBattleStartInteraction(BattleStartSignal signal)
        {
            m_TurnsQueue = CreateTurnQueue(signal);
            yield return ProccessNextTurn();
        }

        private IEnumerator ProccessNextTurn()
        {
            if (m_TurnsQueue.Count == 0) { yield break; }
            var turnTaker = m_TurnsQueue.Dequeue();
            m_TurnsQueue.Enqueue(turnTaker);
            
            turnTaker.SendTurnNotification();
        }

        private List<ITurnTaker> CreateTurnQueue(BattleStartSignal signal)
        {
            var enemies = signal.GetEnemiesInBattle();
            var player = signal.GetPlayerInBattle();

            var temp = new List<ITurnTaker>();

            foreach (var e in enemies)
            {
                temp.Add(new EnemyTurnTaker(m_SignalBus, e));
            }
            temp.Add(new PlayerTurnTaker(m_SignalBus, player));

            return TurnsUtility.CreateTurnsQueue(temp);
        }

    }
}
