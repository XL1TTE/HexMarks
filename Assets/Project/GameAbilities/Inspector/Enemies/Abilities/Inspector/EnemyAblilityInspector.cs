using System;
using System.Collections;
using Project.Enemies;
using XL1TTE.Animator;

namespace Project.Enemies.Abilities
{
    [Serializable]
    public abstract class EnemyAblilityInspector{
        public abstract IEnumerator ExecuteAbility(EnemyView enemy);
    }
}
