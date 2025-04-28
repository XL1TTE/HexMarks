using System.Collections.Generic;
using Project.JobSystem;

namespace Project.Cards{
    public class CardModel
    {
        public JobSequence GetCardExecutionSequence(CardView a_cardView){
            return new JobSequence(
                new List<Job>{
                   new JobPlayCardAnimation(a_cardView),
                   new JobApplyCardEffects()
                });
        }
        
    }
}


