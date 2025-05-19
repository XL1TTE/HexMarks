using System;
using System.Collections;
using UnityEngine;
using XL1TTE.Animator;
using XL1TTE.GameActions;

namespace Project.Enemies.Abilities{
    [Serializable]
    public class EnemyAbilitiesSequence : EnemyAblilityInspector
    {
        [SerializeReference, SubclassSelector] public EnemyAblilityInspector[] abilities;
        public override IEnumerator ExecuteAbility(EnemyView enemy, ContextResolver resolver)
        {
            foreach(var a in abilities)
            {
                yield return a.ExecuteAbility(enemy, resolver);
            }
        }
    }
}
