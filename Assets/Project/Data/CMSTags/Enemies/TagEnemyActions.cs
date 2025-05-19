using System;
using System.Collections;
using CMSystem;
using Project.Enemies;
using Project.Enemies.Abilities;
using UnityEngine;
using XL1TTE.Animator;
using XL1TTE.GameActions;

namespace Project.Data.CMS.Tags.Enemies{

    [Serializable]
    public class TagOnTurnAbilities : EntityComponentDefinition
    {
        [SerializeReference, SubclassSelector] EnemyAblilityInspector m_Ability;
        public IEnumerator ExecuteOnTurnAbilities(EnemyView enemyView, ContextResolver resolver){
            yield return m_Ability.ExecuteAbility(enemyView, resolver);
        }
    }
}
