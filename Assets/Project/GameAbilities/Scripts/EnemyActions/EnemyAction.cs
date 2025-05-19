using System;
using System.Collections;
using System.Collections.Generic;
using Project.Actors;
using Project.Enemies;

namespace XL1TTE.GameActions
{
    [Serializable]
    public abstract class EnemyAction : IContextResolverUser{
        public abstract IEnumerator Execute(EnemyView enemy, Context context);

        public abstract IEnumerable<DataRequest> GetRequests();
    }
}
