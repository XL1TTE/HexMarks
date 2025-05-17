using System;
using System.Collections;
using Project.Cards;
using UnityEngine;

namespace CardAbilities.Inspector{
    [Serializable]
    public class CardAbilitiesSequence : CardAbilityInspector
    {
        [SerializeReference, SubclassSelector] public CardAbilityInspector[] abilities;
        public override IEnumerator ExecuteAbility(CardView card)
        {
            foreach (var a in abilities)
            {
                yield return a.ExecuteAbility(card);
            }
        }
    }
}
