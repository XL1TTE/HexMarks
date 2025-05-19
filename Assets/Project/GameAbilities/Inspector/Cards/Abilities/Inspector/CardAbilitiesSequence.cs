using System;
using System.Collections;
using Project.Cards;
using UnityEngine;
using XL1TTE.GameActions;

namespace CardAbilities.Inspector{
    [Serializable]
    public class CardAbilitiesSequence : CardAbilityInspector
    {
        [SerializeReference, SubclassSelector] public CardAbilityInspector[] abilities;
        public override IEnumerator ExecuteAbility(Card card, ContextResolver resolver)
        {
            foreach (var a in abilities)
            {
                yield return a.ExecuteAbility(card, resolver);
            }
        }
    }
}
