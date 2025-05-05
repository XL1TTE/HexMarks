using System;
using System.Collections.Generic;
using DG.Tweening;
using Project.DataResolving;
using Project.Enemies;
using Project.JobSystem;
using Project.Player;
using Project.Utilities.Extantions;
using Project.Utilities.Tooltips;
using TMPro;
using UnityEngine;

namespace Project.Cards.Effects{
    
    [Serializable]
    public class FireDamageEffect : ICardEffect
    {
        public override IReadOnlyList<DataRequierment> GetDataRequests()
        {
            return new List<DataRequierment>{
              new DataRequierment("EnemyTarget", typeof(EnemyView))  
            };
        }

        public override Job GetJob(CardView cardView, DataContext context)
        {
            var target = context.Get<EnemyView>("EnemyTarget");
            
            var anim = new ColorWithTextCardAnimation("#ff9115".ToColor(), "Fire Damage!").GetAnimation(cardView);

            JobSequence job = new JobSequence(new List<Job>{
                new JobApplyCardEffects(target, 25),
                anim,
            });
            
            return job;
        }
    }
}
