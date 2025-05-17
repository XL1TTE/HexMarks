using System;
using System.Collections;
using UnityEngine;
using XL1TTE.Animator;

namespace Project.Enemies.Abilities{
    [Serializable]
    public class EnemyAbilitiesSequence : EnemyAblilityInspector
    {
        [SerializeReference, SubclassSelector] public EnemyAblilityInspector[] abilities;
        public override IEnumerator ExecuteAbility(EnemyView enemy)
        {
            foreach(var a in abilities)
            {
                yield return a.ExecuteAbility(enemy);
            }
        }
    }
}
