using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardTags;
using Project.Cards;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.JobSystem;
using Project.Wrappers;
using UnityEngine;
using XL1TTE.GameActions;
using Zenject;

namespace Project.Game.Battle.Controllers
{
    public class CardsExecutionController: MonoBehaviour{
        
        [Inject]
        private void Construct(SignalBus signalBus, ContextResolver contextResolver){
            m_SignalBus = signalBus;
            m_ContextResolver = contextResolver;
        }
        private SignalBus m_SignalBus;
        private ContextResolver m_ContextResolver;
        
        private List<AwaitableCoroutine> m_ExecutingCardsRoutines = new();

        void OnEnable()
        {
            m_SignalBus.Subscribe<CardPlayedSignal>(OnCardUsedInteraction);
        }

        void OnDisable()
        {
            m_SignalBus.Unsubscribe<CardPlayedSignal>(OnCardUsedInteraction);
        }

        public bool IsAnyCardExecuting(){
            return m_ExecutingCardsRoutines.Any(c => !c.IsDone);
        }

        private void OnCardUsedInteraction(CardPlayedSignal signal)
        {
            Card card = signal.card;
            
            IEnumerator cardUseRoutine = OnCardPlayed(card).Proccess();

            m_ExecutingCardsRoutines.Add(new AwaitableCoroutine(this, cardUseRoutine));            
        }

        private JobSequence OnCardPlayed(Card card)
        {
            var jobs = new List<Job>();

            // Disable interaction while card playing...
            jobs.Add(new JobSwitchColliderEnabledState(card.m_view.gameObject, false));

            // Play all card abilities
            if (card.m_state.model.Is<TagOnPlayAbilities>(out var onPlayed))
            {
                jobs.Add(new JobPlayRoutine(onPlayed.ExecuteOnPlayAbilities(card, m_ContextResolver)));
            }

            // Enable interactions when all card abilities have played...
            jobs.Add(new JobSwitchColliderEnabledState(card.m_view.gameObject, true));

            // Return card to pool...
            jobs.Add(new JobReturnCardViewToPool(card.m_view));

            return new JobSequence(jobs);
        }


        void Update()
        {
            if(m_ExecutingCardsRoutines.Count != 0){
                var completed = m_ExecutingCardsRoutines.FindAll(c => c.IsDone);
                foreach(var t in completed){
                    t.Kill();
                    m_ExecutingCardsRoutines.Remove(t);
                }
            }
        }
    }
}
