using System.Collections;
using Project.Cards;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.Layouts;
using UnityEngine;
using Zenject;

namespace Project.Game.Battle.Controllers
{
    public class PlayerHandController : MonoBehaviour
    {
        [Inject]
        private void Construct(SignalBus signalBus, ICardFactory cardFactory)
        {
            m_SignalBus = signalBus;
            m_CardFactory = cardFactory;
        }

        void OnEnable()
        {
            m_SignalBus.Subscribe<BattleStartSignal>(OnBattleStartInteraction);
            m_SignalBus.Subscribe<HeroTurnSignal>(OnPlayerTurnInteraction);
            m_SignalBus.Subscribe<EnemyTurnSignal>(OnEnemyTurnInteraction);
        }

        void OnDisable()
        {
            m_SignalBus.Unsubscribe<BattleStartSignal>(OnBattleStartInteraction);
            m_SignalBus.Unsubscribe<HeroTurnSignal>(OnPlayerTurnInteraction);
            m_SignalBus.Unsubscribe<EnemyTurnSignal>(OnEnemyTurnInteraction);

        }


        #region Services
        private SignalBus m_SignalBus;
        private ICardFactory m_CardFactory;
        #endregion

        #region State
        #endregion
        
        [SerializeField] private CardHand m_CardsHand;

        private IEnumerator OnBattleStartInteraction(BattleStartSignal signal)
        {
            yield return null;
        }

        
        private IEnumerator OnPlayerTurnInteraction(HeroTurnSignal signal)
        {
            yield return UpdatePlayerHand();
            TurnOnCardsDragging();
        }
        private IEnumerator OnEnemyTurnInteraction(EnemyTurnSignal signal)
        {
            TurnOffCardsDragging();
            yield return null;
        }


        private void TurnOnCardsDragging(){
            foreach (var card in m_CardsHand.GetAllItems())
            {
                card.EnableDragging();
            }
        }
        private void TurnOffCardsDragging(){
            foreach (var card in m_CardsHand.GetAllItems())
            {
                card.DisableDragging();
            }
        }

        private IEnumerator UpdatePlayerHand()
        {
            m_CardsHand.ClearHand();
            DrawCards(5);
            yield return null;
        }

        private void DrawCards(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Card card = m_CardFactory.CreateNewCard();

                m_CardsHand.TryClaim(card.GetView());
            }
        }

    }
}
