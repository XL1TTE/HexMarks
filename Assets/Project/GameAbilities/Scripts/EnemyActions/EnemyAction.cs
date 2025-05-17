using System;
using System.Collections;
using Project.Enemies;

namespace XL1TTE.GameActions
{
    [Serializable]
    public abstract class EnemyAction{
        public abstract IEnumerator Execute(EnemyView enemy);
    }
}
