using System;
using System.Collections.Generic;
using Project.DataResolving;
using Project.JobSystem;

namespace Project.Cards.Effects{
    
    [Serializable]
    public abstract class ICardEffect : IDataResolverUser
    {
        public abstract IReadOnlyList<DataRequierment> GetDataRequests();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>JobSequence for execution of effect.</returns>
        public abstract Job GetJob(CardView cardView, DataContext context);
    }
}
