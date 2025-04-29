using System.Collections.Generic;
using Project.DataResolving;
using Project.JobSystem;

namespace Project.Cards.Effects{

    public abstract class ICardEffect : IDataResolverUser
    {
        public abstract IReadOnlyList<DataRequierment> GetDataRequests();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>JobSequence for execution of effect.</returns>
        public abstract JobSequence GetJob(CardView cardView, DataContext context);
    }
}
