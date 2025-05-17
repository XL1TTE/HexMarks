using System;
using System.Collections;
using Project.Cards;

namespace CardAbilities.Inspector{
    
    [Serializable]
    public abstract class CardAbilityInspector{
        public abstract IEnumerator ExecuteAbility(CardView card);
    }
}
