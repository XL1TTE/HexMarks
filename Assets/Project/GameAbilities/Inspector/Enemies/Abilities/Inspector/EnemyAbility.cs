using System;
using System.Collections;
using Project.Enemies;
using UnityEngine;
using XL1TTE.Animator;
using XL1TTE.GameAbilities;
using XL1TTE.GameActions;

namespace Project.Enemies.Abilities{
    
    [Serializable]
    public class EnemyAbility : EnemyAblilityInspector
    {
        [SerializeField] public EnemyAbilityDefenition defenition;

        public override IEnumerator ExecuteAbility(EnemyView enemy, ContextResolver resolver)
        {
              yield return defenition.GetAbility(enemy, resolver).Play().WaitForCompletion();  
        }
    }
}
