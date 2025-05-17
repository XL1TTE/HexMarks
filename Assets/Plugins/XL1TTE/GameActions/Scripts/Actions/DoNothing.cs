using System;

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
}
