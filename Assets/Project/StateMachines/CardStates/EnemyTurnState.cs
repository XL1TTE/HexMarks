
using Project.Cards;

namespace Project.StateMachines.CardStates{
    
    public abstract class ICardState{
        
        public abstract void Enter(CardView card);
        public abstract void Exit(CardView card);
    }

    public class EnemyTurnState : ICardState
    {
        public override void Enter(CardView card)
        {
            card.DisableDragging();
            var interactions = card.GetInteractions();
            interactions.DisableOnHoverHighlight();
            interactions.SetCardStateToDefault();
        }

        public override void Exit(CardView card)
        {
            card.EnableDragging();
            var interactions = card.GetInteractions();
            interactions.EnableOnHoverHighlight();
        }
    }

    public class PlayerTurnState : ICardState
    {
        public override void Enter(CardView card)
        {
            card.EnableDragging();
        }

        public override void Exit(CardView card)
        {
            card.DisableDragging();
        }
        
        
    }
}
