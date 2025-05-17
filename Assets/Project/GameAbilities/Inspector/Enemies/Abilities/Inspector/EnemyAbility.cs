using System;
using System.Collections;
using Project.Enemies;
using UnityEngine;
using XL1TTE.Animator;
using XL1TTE.GameAbilities;

namespace Project.Enemies.Abilities{
    
    [Serializable]
    public class EnemyAbility : EnemyAblilityInspector
    {
        [SerializeField] public EnemyAbilityDefenition defenition;

        public override IEnumerator ExecuteAbility(EnemyView enemy)
        {
              yield return defenition.GetAbility(enemy).Play().WaitForCompletion();  
        }
    }
}
