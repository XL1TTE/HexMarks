using System;
using System.Collections.Generic;
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
        
        public JobSequence GetCardExecutionSequence(CardView a_cardView, DataRosolver a_resolver){

            var jobs = new List<Job>();

            // Gets all effect's jobs.
            foreach (var effect in m_Effects){
                
                var ExContext = a_resolver.Resolve(effect);

                jobs.Add(effect.GetJob(a_cardView, ExContext));
            }
            
            return new JobSequence(jobs);
        }
        
    }
}


