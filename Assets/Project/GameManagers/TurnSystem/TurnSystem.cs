using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Project.Enemies;
using Project.EventBus;
using Project.Player;
using Project.Utilities.Extantions;
using UnityEditor.ShaderKeywordFilter;

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
