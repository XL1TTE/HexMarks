using System;
using System.Collections;
using System.Collections.Generic;
using Project.Actors;
using Project.Enemies;

namespace XL1TTE.GameActions
{
    [Serializable]
    public class EnemyDoNothing : EnemyAction
    {
        public override IEnumerator Execute(EnemyView enemy, Context context)
        {
            yield break;
        }

        public override IEnumerable<DataRequest> GetRequests()
        {
            return new List<DataRequest>();
        }
    }
}
