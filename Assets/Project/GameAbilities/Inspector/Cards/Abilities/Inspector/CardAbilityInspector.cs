using System;
using System.Collections;
using Project.Cards;
using XL1TTE.GameActions;

namespace CardAbilities.Inspector{
    
    [Serializable]
    public abstract class CardAbilityInspector{
        public abstract IEnumerator ExecuteAbility(Card card, ContextResolver resolver);
    }
}
