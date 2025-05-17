using System;
using System.Collections.Generic;
using System.Data;
using DG.Tweening;
using Project.DataResolving;
using Project.Enemies;
using Project.JobSystem;
using Project.Utilities.Extantions;
using Project.Utilities.Tooltips;
using TMPro;
using UnityEngine;
using XL1TTE.GameActions;

namespace Project.Cards.Effects{
    
    [Serializable]
    public class ColdDamageEffect : ICardEffect
    {
        public override IEnumerable<DataRequest> GetRequests()
        {
            return new List<DataRequest>{
                new DataRequest("EnemyTarget", typeof(EnemyView))  
            };
        }

        public override Job GetJob(CardView cardView, Context context)
        {
            
            var target = context.Get<EnemyView>("EnemyTarget");
            

            var anim = new ColorWithTextCardAnimation(Color.blue, "Cold Damage!").GetAnimation(cardView);

            JobSequence job = new JobSequence(new List<Job>{
                new JobApplyCardEffects(target, 5),
                anim
            });   
            
            return job;                   
        }
    }
}
