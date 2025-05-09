using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using Project.Cards;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Utilities.Extantions;
using Project.Wrappers;
using UnityEngine;
using Zenject;

namespace Project.Game.Battle.Controllers
{
    public class CardsExecutionController: MonoBehaviour{
        
        [Inject]
        private void Construct(SignalBus signalBus){
            m_SignalBus = signalBus;
        }
        private SignalBus m_SignalBus;
        
        private List<AwaitableCoroutine> m_ExecutingCardsRoutines = new();

        void OnEnable()
        {
            m_SignalBus.Subscribe<CardUsedSignal>(OnCardUsedInteraction);
        }

        void OnDisable()
        {
            m_SignalBus.Unsubscribe<CardUsedSignal>(OnCardUsedInteraction);
        }

        public bool IsAnyCardExecuting(){
            return m_ExecutingCardsRoutines.Any(c => !c.IsDone);
        }

        private IEnumerator OnCardUsedInteraction(CardUsedSignal signal)
        {
            var cardView = signal.GetCardView();
            var card = cardView.GetCardController();
            
            IEnumerator cardUseRoutine = card.GetCardUseSequence();

            m_ExecutingCardsRoutines.Add(new AwaitableCoroutine(this, cardUseRoutine));
            
            yield return null;
        }

        void Update()
        {
            m_ExecutingCardsRoutines.RemoveAll(c => c.IsDone);
        }
    }
}
