using System;
using System.Collections.Generic;
using Project.DataResolving;
using Project.JobSystem;
using XL1TTE.GameActions;

namespace Project.Cards.Effects{
    
    [Serializable]
    public abstract class ICardEffect : IContextResolverUser
    {
        public abstract IEnumerable<DataRequest> GetRequests();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>JobSequence for execution of effect.</returns>
        public abstract Job GetJob(CardView cardView, Context context);
    }
}
