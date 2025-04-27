using Project.Cards;
using Project.Factories;
using Project.Layouts;
using UnityEngine;
using Zenject;

namespace Project
{
    public class Testing : MonoBehaviour
    {
        
        /////////PLAN//////////
        /// (-) Make levels with enemy prototypes (enemy - just placeholders)
        /// (-) Make battle controller with sequence of levels
        /// (-) Make battle controller to spawn enemies from levels in specified transforms
        /// (-) Make Jobs system to proccess simple card disappear animations on use
        /// (-) Make jobs sequence with 1) PlayCardAnim job, 2) LogCardEffectApply job
        /// (-) Make simple state machine for battle controller 1) enemy turn state, 2) player turn state
        /// (-) Make placeholder for state switching, just for testing
        /// (-) Make simple PlayerData with Health at least
        /// (-) Make some ui representation of player health
        /// (-) Make some sort of turns system - queue or something...
        /// (-) Make OnCardPlayed event to notify battle controller
        /// (-) Make enemies that will damage player on their turn (turn changed after player plays one card)
        /// (-) Make ability for card that will actually do damage to enemy
        /// (-) Make tooltip for enemy health on pointer hover
        /// (-) Make enemy die handler in battle controller
        
        [Inject]
        private void Construct(ICardFactory a_cardFactory){
            m_cardFactory = a_cardFactory;
        }
        
        ICardFactory m_cardFactory;
        [SerializeField] CardHand m_cardHand;
        
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)){
                Card card = m_cardFactory.CreateNewCard();
                
                m_cardHand.Claim(card);
            }
        }
    }
}
