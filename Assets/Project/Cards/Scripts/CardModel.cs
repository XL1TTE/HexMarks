using System.Collections.Generic;
using Project.Cards.Effects;
using Project.DataResolving;
using Project.JobSystem;

namespace Project.Cards{
    public class CardModel
    {
        private List<ICardEffect> m_Effects = new List<ICardEffect>{new ColdDamageEffect(), new FireDamageEffect()};
        
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


