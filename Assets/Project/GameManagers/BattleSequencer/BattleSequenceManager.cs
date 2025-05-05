using System;
using System.Collections;
using System.Collections.Generic;
using Project.Cards;
using Project.DataResolving;
using Project.Enemies;
using Project.Enemies.AIs;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.Game.Battle.UI;
using Project.GameManagers.BattleSequence;
using Project.Layouts;
using Project.Player;
using Project.TurnSystem;
using Project.Utilities.Extantions;
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
            InitializeUI();

            m_EnemySequencer = new EnemyInBattleSequencer(m_SignalBus, this);

            m_SignalBus.Subscribe<BattleStartSignal>(OnBattleStarted);
            
            m_SignalBus.Subscribe<EnemyDiedSignal>(OnEnemyDied);

            m_SignalBus.Subscribe<PlayerTurnSignal>(OnPlayerTurn);
            m_SignalBus.Subscribe<EnemyTurnSignal>(OnEnemyTurn);
        }

        [SerializeReference] private CardHand m_CardsHand;
        [SerializeReference] private BattleUI m_BattleUI; 
        
        private PlayerInBattle m_PlayerInBattle;
        private SignalBus m_SignalBus;
        private DataRosolver m_DataResolver;
        private ICardFactory m_CardFactory;

        
        
        private List<EnemyView> m_EnemiesInBattle = new();
        private List<ITurnTaker> m_TurnsQueue;


        #region BattleSequencers

        private EnemyInBattleSequencer m_EnemySequencer;

        #endregion

        private void OnBattleStarted(BattleStartSignal signal)
        {
            m_EnemiesInBattle = signal.GetEnemiesInBattle();
            m_PlayerInBattle = signal.GetPlayerInBattle();
            
            m_PlayerInBattle.OnDamageTaken += OnPlayerDamageTaken;            

            m_TurnsQueue = CreateTurnQueue(signal);


            // Initial notification
            m_SignalBus.SendSignal(new PlayerHealthChangedSingal(m_PlayerInBattle));

            UpdatePlayerHand();

            ProccessTurn();
        }
        
        private void InitializeUI(){
            m_BattleUI.AddEndTurnBtnOnClickListener(ProccessTurn);
        }
        
        private List<ITurnTaker> CreateTurnQueue(BattleStartSignal signal)
        {
            var temp = new List<ITurnTaker>();

            foreach (var e in signal.GetEnemiesInBattle())
            {
                temp.Add(new EnemyTurnTaker(m_SignalBus, e));
            }
            temp.Add(new PlayerTurnTaker(m_SignalBus, m_PlayerInBattle));

            return TurnsUtility.CreateTurnsQueue(temp);
        }
        private void UpdatePlayerHand(){
            
            ClearHand();
            DrawCards(5);
        }
        
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

        private void ProccessTurn(){
            if(m_TurnsQueue.Count == 0){return;}

            var turnTaker = m_TurnsQueue.Dequeue();
            m_TurnsQueue.Enqueue(turnTaker);
            turnTaker.SendTurnNotification();
        }
        
        private void OnPlayerTurn(PlayerTurnSignal signal){

            // Enable dragging for every card on enemy turn...
            foreach (var card in m_CardsHand.GetAllItems())
            {
                card.EnableDragging();
            }

            UpdatePlayerHand();
            
        } 
        
        private void OnEnemyTurn(EnemyTurnSignal signal){
            StartCoroutine(OnEnemyTurnRoutine(signal));
        }
        
        private IEnumerator OnEnemyTurnRoutine(EnemyTurnSignal signal)
        {
            // Disable dragging for every card on enemy turn...
            foreach (var card in m_CardsHand.GetAllItems())
            {
                card.DisableDragging();
            }

            EnemyAI enemyAI = signal.GetEnemy().GetController().GetAI();

            yield return DoEnemyTurnRoutine(enemyAI);

            ProccessTurn();
        }
        
        private IEnumerator DoEnemyTurnRoutine(EnemyAI enemyAI){
            
            DataContext aiContext = m_DataResolver.Resolve(enemyAI);

            yield return enemyAI.GetAITurnSequence(aiContext);
        }

        private void OnPlayerDamageTaken(PlayerInBattle player)
        {
            m_SignalBus.SendSignal(new PlayerHealthChangedSingal(player));

            if (player.GetCurrentHealth() == 0)
            {
                m_SignalBus.SendSignal(new PlayerLostBattleSignal());
            }
        }
        
        private void OnEnemyDied(EnemyDiedSignal signal){
            var enemy = signal.GetEnemy();
            
            m_TurnsQueue.TryRemove((t) => t is EnemyTurnTaker tt && tt.GetEnemy() == enemy);
            m_EnemiesInBattle.Remove(enemy);

            if(CheckWinConditions()){
                m_SignalBus.SendSignal(new PlayerWonBattleSignal());
            }
        }
        
        private bool CheckWinConditions(){
            if(m_EnemiesInBattle.Count == 0){
                return true;
            }
            return false;
        }
    }
}
