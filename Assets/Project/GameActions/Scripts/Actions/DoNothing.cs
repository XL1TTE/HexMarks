using System;
using System.Collections;
using Project.Enemies;

namespace XL1TTE.GameActions
{
    [Serializable]
    public class DoNothing : GameAction
    {
        public override void Execute()
        {
            return;
        }
    }

    [Serializable]
    public class EnemyDoNothing : EnemyAction
    {
        public override IEnumerator Execute(EnemyView enemy)
        {
            yield break;
        }
    }
}
