using System.Collections.Generic;
using Project.Cards;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.Game.Battle.UI;
using Project.Layouts;
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
        /// (-) Make simple PlayerData with Health at least
        /// (-) Make some ui representation of player health
        /// (-) Make some sort of turns system - queue or something...
        /// (-) Make OnCardPlayed event to notify battle controller
        /// (-) Make enemies that will damage player on their turn (turn changed after player plays one card)
        /// (-) Make ability for card that will actually do damage to enemy
        /// (-) Make tooltip for enemy health on pointer hover
        /// (-) Make enemy die handler in battle controller
        void Awake()
        {
            stateMachine = new BattleStateMachine(this);
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
        
        [SerializeField] private BattleUI m_BattleUI;
        public BattleUI GetUI() => m_BattleUI;        
        BattleStateMachine stateMachine;
        
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)){
                Card card = m_cardFactory.CreateNewCard();
                
                m_cardHand.TryClaim(card.GetView());
            }
            if(Input.GetKeyDown(KeyCode.LeftBracket)){
                stateMachine.ChangeState<StateMachines.BattleStateMachine.EnemyTurnState>();
                m_SignalBus.SendSignal(new BattleStateChanged("Enemy turn!"));
            }
            if(Input.GetKeyDown(KeyCode.RightBracket)){
                stateMachine.ChangeState<StateMachines.BattleStateMachine.PlayerTurnState>();
                m_SignalBus.SendSignal(new BattleStateChanged("Your turn!"));

            }
        }
    }
}
