using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.EventBus;
using Project.EventBus.Signals;
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

        private void OnCardUsedInteraction(CardUsedSignal signal)
        {
            var card = signal.GetCardView();
            
            IEnumerator cardUseRoutine = card.OnCardPlayed().Proccess();

            m_ExecutingCardsRoutines.Add(new AwaitableCoroutine(this, cardUseRoutine));
            
        }

        void Update()
        {
            if(m_ExecutingCardsRoutines.Count != 0){
                m_ExecutingCardsRoutines.RemoveAll(c => c.IsDone);
            }
        }
    }
}
