using System.Collections.Generic;
using System.Linq;
using Project.Utilities.Extantions;

namespace Project.TurnSystem{
    public static class TurnsUtility{
        
        public static List<ITurnTaker> CreateTurnsQueue(List<ITurnTaker> turnTakers)
        {
            var TurnsQueue = new List<ITurnTaker>();

            var orderedTurnTakers = turnTakers.OrderByDescending(e => e.GetInitiative());

            foreach (var t in orderedTurnTakers)
            {
                TurnsQueue.Enqueue(t);
            }
            
            return TurnsQueue;
        }
    }
}
