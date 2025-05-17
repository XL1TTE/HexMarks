using System;
using UnityEngine;
using XL1TTE.Animator;
using XL1TTE.GameAbilities;

namespace Project.Enemies.Abilities
{
    [Serializable]
    internal class EnemyAbilityProbabilityPair
    {
        [SerializeField, Range(0, 10000)] public int weight;
        [SerializeField] public EnemyAbilityDefenition ability;
    }
}
