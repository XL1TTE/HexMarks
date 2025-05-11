using System;
using System.Collections;
using System.Collections.Generic;
using Project.DataResolving;
using UnityEngine;

namespace Project.Enemies.AI{
    
    [Serializable]
    public abstract class BaseEnemyAction : IDataResolverUser
    {
        public abstract IReadOnlyList<DataRequierment> GetDataRequests();
        public abstract IEnumerator GetAction(EnemyView state, DataContext context);
    }

    [Serializable]
    public class AttackPlayer : BaseEnemyAction
    {
        public override IReadOnlyList<DataRequierment> GetDataRequests()
        {
            return new List<DataRequierment>{
            };
        }

        public override IEnumerator GetAction(EnemyView state, DataContext context)
        {

            var enemy = state.GetController();

            Debug.Log($"Damaging Player with: {enemy.GetEnemyDamage()} damage.");

            state.StopIdleAnimation();

            // plays attack animation
            yield return enemy.GetAttackAnimation();

            state.StartIdleAnimation();
        }
    }
    

}
