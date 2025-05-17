using System;
using System.Collections;
using System.Collections.Generic;
using CardAbilities.Inspector;
using CMSystem;
using Project.Cards;
using UnityEngine;
using XL1TTE.Animator;
using XL1TTE.GameAbilities;

namespace CardTags{
    
    [Serializable]
    public class TagOnPlayAbilities : EntityComponentDefinition{
        [SerializeReference, SubclassSelector] private CardAbilityInspector m_defenition;
        public IEnumerator ExecuteOnPlayAbilities(CardView card){
            yield return m_defenition.ExecuteAbility(card);
        }
    }
    
}
