
using System;
using System.Collections.Generic;
using Project.Cards;

namespace Project.StateMachines.BattleStateMachine{
    
    public class BattleStateMachine{
        
        public BattleStateMachine(Testing test){
            m_test = test;
        }
        Testing m_test;
        
        IBattleState m_CurrentState;
        
        public void ChangeState<T>() where T: IBattleState{
            var state = Activator.CreateInstance(typeof(T)) as IBattleState;
            if(state == null){return;}

            var context = new BattleStateContext(m_test.GetCardsInHand());

            m_CurrentState?.Exit(context);
            
            state.Enter(context);
            m_CurrentState = state;
        }
        
    }
    
    public class BattleStateContext{
        public BattleStateContext(
            IEnumerable<CardView> Cards
        )
        {
            m_Cards = Cards;
        }
        public IEnumerable<CardView> m_Cards;
    }
    
    public abstract class IBattleState{
        public abstract void Enter(BattleStateContext context);
        public abstract void Exit(BattleStateContext context);
    }

    public class EnemyTurnState : IBattleState
    {
        CardStates.EnemyTurnState CardState = new CardStates.EnemyTurnState();
        public override void Enter(BattleStateContext context)
        {
            foreach(var card in context.m_Cards){
                CardState.Enter(card);
            }
        }

        public override void Exit(BattleStateContext context)
        {
            foreach (var card in context.m_Cards)
            {
                CardState.Exit(card);
            }
        }
    }
    public class PlayerTurnState : IBattleState
    {
        CardStates.PlayerTurnState CardState = new CardStates.PlayerTurnState();
        public override void Enter(BattleStateContext context)
        {
            foreach(var card in context.m_Cards){
                CardState.Enter(card);
            }
        }

        public override void Exit(BattleStateContext context)
        {
            foreach (var card in context.m_Cards)
            {
                CardState.Exit(card);
            }
        }
    }
}
