using System;
using System.Collections;
using Project.Cards;
using UnityEngine;
using XL1TTE.Animator;
using XL1TTE.GameAbilities;

namespace CardAbilities.Inspector{
    [Serializable]
    public class CardAbility : CardAbilityInspector
    {
        [SerializeField] private CardAbilityDefenition m_defention;
        public override IEnumerator ExecuteAbility(CardView card)
        {
            yield return m_defention.GetAbility(card).Play().WaitForCompletion();
        }
    }
}
