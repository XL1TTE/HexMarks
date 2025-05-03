using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Cards;
using Project.DataResolving;
using Project.Enemies;
using Project.Enemies.AIs;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.JobSystem;
using Project.Layouts;
using Project.StateMachines.BattleStateMachine;
using Project.TurnSystem;
using Project.Wrappers;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Project.GameManagers{
    public class BattleSequenceManager: MonoBehaviour{


        [Inject]
        private void Construct(SignalBus signalBus, DataRosolver resolver, ICardFactory cardFactory)
        {
            m_SignalBus = signalBus;
            m_DataResolver = resolver;
            m_CardFactory = cardFactory;
        }
        public void Enable()
        {
            m_BattleStateMachine = new BattleStateMachine(this);

            m_SignalBus.Subscribe<EnemySpawnedSignal>(OnEnemySpawned);
            m_SignalBus.Subscribe<CardUsedOnEnemySignal>(OnCardPlayedOnEnemy);
            m_SignalBus.Subscribe<BattleStartSignal>(OnBattleStarted);
            
            //Turns
            
            m_SignalBus.Subscribe<PlayerTurnSignal>(OnPlayerTurn);
            m_SignalBus.Subscribe<EnemyTurnSignal>(OnEnemyTurn);
        }

        public void Disable()
        {
            m_SignalBus.Unsubscribe<EnemySpawnedSignal>(OnEnemySpawned);
            m_SignalBus.Unsubscribe<CardUsedOnEnemySignal>(OnCardPlayedOnEnemy);
            m_SignalBus.Unsubscribe<BattleStartSignal>(OnBattleStarted);

            //Turns
            m_SignalBus.Unsubscribe<PlayerTurnSignal>(OnPlayerTurn);
            m_SignalBus.Unsubscribe<EnemyTurnSignal>(OnEnemyTurn);
        }
        
        [SerializeReference] private CardHand m_CardsHand;
        
        private SignalBus m_SignalBus;
        private DataRosolver m_DataResolver;
        private ICardFactory m_CardFactory;

        private Dictionary<EnemyView, List<AwaitedCoroutine>> m_enemiesRoutinesToAwait = new();
        
        private Queue<ITurnTaker> m_TurnsQueue = new();
        private BattleStateMachine m_BattleStateMachine;

        private void OnEnemySpawned(EnemySpawnedSignal signal)
        {
            signal.GetEnemy().GetController().OnDamageTaken += OnEnemyDamageTaken;
        }

        private void OnEnemyDamageTaken(EnemyView view)
        {
            m_SignalBus.SendSignal(new EnemyHealthChangedSignal(view));

            if (view.GetController().GetCurrentHealth() == 0)
            {
                StartCoroutine(EnemyDeathRoutine(view));
            }
        }

        private IEnumerator EnemyDeathRoutine(EnemyView enemy){
            
            var enemyDieSequence = enemy.GetController().GetDieSequence();

            if (!m_enemiesRoutinesToAwait.TryGetValue(enemy, out var awaiters))
            {
                yield return enemyDieSequence;
                yield break;
            }
            
            while (awaiters.Any(w => !w.IsDone))
            {
                yield return null;
            }
            
            m_enemiesRoutinesToAwait.Remove(enemy);
            
            yield return enemyDieSequence;
            
            m_SignalBus.SendSignal(new EnemyDiedSignal(enemy));
            
            Destroy(enemy.gameObject);
        }
        
        private void OnCardPlayedOnEnemy(CardUsedOnEnemySignal signal){
            var card = signal.GetCardView();
            var enemy = signal.GetTarget();

            // Launch card use sequence and adds it to awaiters of enemy
            var cardUsingAwaiter = new AwaitedCoroutine(this, card.GetCardUseSequence());


            if (m_enemiesRoutinesToAwait.TryGetValue(enemy, out var awaiters)){
               awaiters.Add(cardUsingAwaiter); 
            }
            else{
                m_enemiesRoutinesToAwait.Add(enemy, new List<AwaitedCoroutine> {cardUsingAwaiter});
            }
            
            TriggerNextTurn();
        }


        private void OnBattleStarted(BattleStartSignal signal)
        {
            StartCoroutine(TestCoroutine(signal));
        }
        
        private IEnumerator TestCoroutine(BattleStartSignal signal)
        {
            
            ClearHand();
            
            
            DrawCards(5);

            m_TurnsQueue.Clear();

            var enemies = signal.GetEnemiesInBattle().OrderBy(e => e.GetController().GetInitiaive());

            var player = signal.GetPlayerInBattle();

            m_TurnsQueue.Enqueue(new PlayerTurnTaker(m_SignalBus, player));

            foreach (var e in enemies)
            {
                m_TurnsQueue.Enqueue(new EnemyTurnTaker(m_SignalBus, e));
            }

            TriggerNextTurn();
            yield return null;
        }

        public IReadOnlyList<CardView> GetCardsInHand() => m_CardsHand.GetAllItems();

        private void DrawCards(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Card card = m_CardFactory.CreateNewCard();

                m_CardsHand.TryClaim(card.GetView());
            }
        }
        
        private void ClearHand(){
            m_CardsHand.ClearHand();
        }

        private void TriggerNextTurn(){
            if(m_TurnsQueue.Count == 0){return;}

            var temp = m_TurnsQueue.Dequeue();
            
            m_TurnsQueue.Enqueue(temp);
            temp.SendTurnNotification();
        }
        
        private void OnPlayerTurn(PlayerTurnSignal signal){
            m_BattleStateMachine.ChangeState<PlayerTurnState>();
        }

        private void OnEnemyTurn(EnemyTurnSignal signal){
            m_BattleStateMachine.ChangeState<EnemyTurnState>();

            EnemyView enemy = signal.GetEnemy();
            EnemyAI enemyAI = enemy.GetController().GetAI();
               
            DataContext aiContext = m_DataResolver.Resolve(enemyAI);    

            StartCoroutine(EnemyTurnRoutine(enemyAI, aiContext));
            
        }
        
        private IEnumerator EnemyTurnRoutine(EnemyAI enemyAI, DataContext aiContext){
            yield return enemyAI.GetAITurnSequence(aiContext);
            TriggerNextTurn();
        }


    }
}
