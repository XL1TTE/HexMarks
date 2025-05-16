using System;
using System.Collections.Generic;
using Project.DataResolving;
using Project.Enemies;
using Project.JobSystem;
using Project.Utilities.Extantions;
using UnityEngine;
using XL1TTE.GameActions;

namespace Project.Cards.Effects{
    
    [Serializable]
    public class AoeOnAllEnemiesFireDamageEffect : ICardEffect
    {
        public override IEnumerable<DataRequest> GetRequests()
        {
            return new List<DataRequest>{
                new DataRequest("EnemiesInBattle", typeof(List<EnemyView>)),
                //new DataRequierment("CardCaster", typeof(PlayerInBattle))
            };
        }

        public override Job GetJob(CardView cardView, Context context)
        {
            var Enemies = context.Get<List<EnemyView>>("EnemiesInBattle");
            //var CardCaster = context.Get<PlayerInBattle>("CardCaster");

            var EnemyBurnAnim = new ColorWithTextEnemyAnimation(Color.white, "#ff9115".ToColor(), "Burning!");

            var job_seq = new List<Job>();

            foreach (var enemy in Enemies){
                job_seq.Add(EnemyBurnAnim.GetAnimation(enemy));
                job_seq.Add(new JobApplyCardEffects(enemy, 25));
            }
            
            return new ParallelJobSequence(job_seq, cardView);
        }
    }
}
