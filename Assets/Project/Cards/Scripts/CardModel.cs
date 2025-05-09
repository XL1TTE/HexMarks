using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Project.Cards.Effects;
using Project.DataResolving;
using Project.JobSystem;
using UnityEngine;

namespace Project.Cards{
    
    [Serializable]
    public class CardModel
    {
        [SerializeField] private Sprite m_CardSprite;
        public Sprite GetCardSprite() => m_CardSprite;
        
        [SerializeReference, SubclassSelector]
        private ICardEffect[] m_Effects = Array.Empty<ICardEffect>();
        
        public JobSequence GetCardExecutionSequence(CardView a_cardView, DataResolver a_resolver){

            var jobs = new List<Job>();

            // Gets all effect's jobs.
            foreach (var effect in m_Effects){
                
                var ExContext = a_resolver.Resolve(effect);

                jobs.Add(effect.GetJob(a_cardView, ExContext));
            }
            
            jobs.Add(new JobPlayRoutine(CardDisappearAnimation(a_cardView)));
            jobs.Add(new JobReturnCardViewToPool(a_cardView));
            jobs.Add(new JobPlayRoutine(ResetCardView(a_cardView)));
            
            return new JobSequence(jobs);
        }
        
        private IEnumerator CardDisappearAnimation(CardView cardView){
            var tween = cardView.GetRenderer().DOFade(0, 0.5f);
            
            yield return tween.WaitForCompletion();
            
            tween.Kill();
        }
        
        private IEnumerator ResetCardView(CardView cardView){
            var tween = cardView.GetRenderer().DOFade(1, 0f);

            yield return tween.WaitForCompletion();

            tween.Kill();
        }
        
    }
}


