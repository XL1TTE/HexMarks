using System;
using System.Collections;
using System.Collections.Generic;
using Project.Enemies;
using Project.Enemies.Abilities;
using UnityEngine;
using XL1TTE.Animator;
using XL1TTE.GameAbilities;

namespace Project.Enemies.Abilities{
    [Serializable]
    public class EnemyAbilitiesFromProbability: EnemyAblilityInspector{
        [SerializeField] List<EnemyAbilityProbabilityPair> settings;
        
        public override IEnumerator ExecuteAbility(EnemyView enemy){
            float total = 0;
            foreach (var v in settings)
            {
                total += v.weight;
            }

            var hit = UnityEngine.Random.Range(0, total);

            float factor = 0;
            EnemyAbilityDefenition choose = null;

            foreach (var v in settings)
            {
                factor += v.weight;
                if (hit <= factor)
                {
                    choose = v.ability;
                    break;
                }
            }
            if (choose != null)
            {
                yield return choose.GetAbility(enemy).Play().WaitForCompletion();
            }
        }
    }
}
