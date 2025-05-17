using System;
using System.Collections;
using Project.Cards;

namespace XL1TTE.GameActions
{
    [Serializable]
    public abstract class CardAction{
        public abstract IEnumerator Execute(CardView card);
    }
}
