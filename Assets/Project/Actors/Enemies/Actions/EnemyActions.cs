using System;
using System.Collections;
using System.Collections.Generic;
using Project.DataResolving;
using Project.Layouts;
using Project.Player;
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
              new DataRequierment("PlayerInBattle", typeof(PlayerInBattle)),
            };
        }

        public override IEnumerator GetAction(EnemyView state, DataContext context)
        {
            var player = context.Get<PlayerInBattle>("PlayerInBattle");

            var enemy = state.GetController();

            Debug.Log($"Damaging Player with: {enemy.GetEnemyDamage()} damage.");

            state.StopIdleAnimation();

            // plays attack animation
            yield return enemy.GetAttackAnimation();

            player.TakeDamage(enemy.GetEnemyDamage());

            state.StartIdleAnimation();
        }
    }

    [Serializable]
    public class DecreasePlayerHandCapacity : BaseEnemyAction
    {
        [SerializeField, Range(1, 10)] int Amount = 1;
        public override IEnumerator GetAction(EnemyView state, DataContext context)
        {
            var player = context.Get<PlayerInBattle>("PlayerInBattle");

            var currentCapacity = player.GetHandCapacity();
            
            if(currentCapacity == 0){yield break;}
            
            player.SetHandCapacity(currentCapacity - Amount);
        }

        public override IReadOnlyList<DataRequierment> GetDataRequests()
        {
            return new List<DataRequierment>{
              new DataRequierment("PlayerInBattle", typeof(PlayerInBattle))
            };
        }
    }

    [Serializable]
    public class IncreasePlayerHandCapacity : BaseEnemyAction
    {
        [SerializeField, Range(1, 10)] int Amount = 1;
        public override IEnumerator GetAction(EnemyView state, DataContext context)
        {
            var player = context.Get<PlayerInBattle>("PlayerInBattle");

            var currentCapacity = player.GetHandCapacity();

            player.SetHandCapacity(currentCapacity + Amount);

            yield return null;
        }

        public override IReadOnlyList<DataRequierment> GetDataRequests()
        {
            return new List<DataRequierment>{
              new DataRequierment("PlayerInBattle", typeof(PlayerInBattle))
            };
        }
    }

    

}
