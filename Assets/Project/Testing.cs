using System;
using System.Collections.Generic;
using Project.Cards;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.Game.Battle.UI;
using Project.Layouts;
using Project.Player;
using Project.StateMachines.BattleStateMachine;
using Project.StateMachines.CardStates;
using UnityEngine;
using Zenject;

namespace Project
{
    public class Testing : MonoBehaviour
    {

        /////////PLAN//////////
        /// (+) Make levels with enemy prototypes (enemy - just placeholders)
        /// (+) Make battle controller with sequence of levels
        /// (+) Make battle controller to spawn enemies from levels in specified transforms
        /// (+) Make Jobs system to proccess simple card disappear animations on use
        /// (+) Make jobs sequence with 1) PlayCardAnim job, 2) LogCardEffectApply job
        /// 
        /// (+) Make object pool for cards view to card factory
        /// 
        /// (+-) Make simple state machine for battle controller 1) enemy turn state, 2) player turn state
        /// (+-) Make placeholder for state switching, just for testing
        /// (+) Make simple PlayerData with Health at least
        /// (+) Make some ui representation of player health
        /// (-) Make some sort of turns system - queue or something...
        /// (-) Make OnCardPlayed event to notify battle controller
        /// (-) Make enemies that will damage player on their turn (turn changed after player plays one card)
        /// (-) Make ability for card to actually damage enemy
        /// (-) Make some kind of health bars above enemies (object pool?)
        /// (-) Make tooltip for enemy health on pointer hover
        /// (-) Make enemy die handler in battle controller
        /// 
        /// 
        /// !!! Make game system in this kind of way - [EnemySpawnedSignal -> EnemiesHealthBarsController 
        /// -> Spawn HealthBars above enemy. EnemyHealthChanged -> EnemiesHealthBarsController -> UpdateHealthBarValue]  
        void Awake()
        {
            stateMachine = new BattleStateMachine(this);
            
            playerData = new PlayerData();
            playerHealthData = new PlayerHealthData(playerData.GetMaxHealth(), playerData.GetMaxHealth());
            m_SignalBus.SendSignal(new PlayerHealthChangedSingal(playerHealthData));
        }

        [Inject]
        private void Construct(ICardFactory a_cardFactory, SignalBus signalBus){
            m_cardFactory = a_cardFactory;
            m_SignalBus = signalBus;
        }
        
        ICardFactory m_cardFactory;
        SignalBus m_SignalBus;
        [SerializeField] CardHand m_cardHand;
        
        public IReadOnlyList<CardView> GetCardsInHand() => m_cardHand.GetAllItems();      
        BattleStateMachine stateMachine;
        
        PlayerData playerData;
        
        IPlayerHealthData playerHealthData;
        public void PlayerTakeDamage(float amount){
            playerHealthData.SetCurrentHealth(Mathf.Max(0, playerHealthData.GetCurrentHealth() - amount));
            m_SignalBus.SendSignal(new PlayerHealthChangedSingal(playerHealthData));
        }
        public void HealPlayer(float amount){
            playerHealthData.SetCurrentHealth(Mathf.Min(playerHealthData.GetMaxHealth(), playerHealthData.GetCurrentHealth() + amount));
            m_SignalBus.SendSignal(new PlayerHealthChangedSingal(playerHealthData));
        }
        
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)){
                Card card = m_cardFactory.CreateNewCard();
                
                m_cardHand.TryClaim(card.GetView());
            }
            if(Input.GetKeyDown(KeyCode.LeftBracket)){
                stateMachine.ChangeState<StateMachines.BattleStateMachine.EnemyTurnState>();
                m_SignalBus.SendSignal(new BattleStateChangedSignal("Enemy turn!"));
            }
            if(Input.GetKeyDown(KeyCode.RightBracket)){
                stateMachine.ChangeState<StateMachines.BattleStateMachine.PlayerTurnState>();
                m_SignalBus.SendSignal(new BattleStateChangedSignal("Your turn!"));

            }
            
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                PlayerTakeDamage(10);
            }
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                HealPlayer(10);
            }
        }
    }
}
