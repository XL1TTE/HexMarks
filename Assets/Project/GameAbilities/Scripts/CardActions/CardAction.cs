using System;
using System.Collections;
using System.Collections.Generic;
using Project.Cards;

namespace XL1TTE.GameActions
{
    [Serializable]
    public abstract class CardAction: IContextResolverUser{
        public abstract IEnumerator Execute(Card card, Context context);

        public abstract IEnumerable<DataRequest> GetRequests();
    }
}
