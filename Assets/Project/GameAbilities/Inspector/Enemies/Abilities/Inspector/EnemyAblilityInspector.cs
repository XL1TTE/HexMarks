using System;
using System.Collections;
using Project.Enemies;
using XL1TTE.Animator;
using XL1TTE.GameActions;

namespace Project.Enemies.Abilities
{
    [Serializable]
    public abstract class EnemyAblilityInspector{
        public abstract IEnumerator ExecuteAbility(EnemyView enemy, ContextResolver resolver);
    }
}
