using System;
using System.Collections.Generic;
using Project.DataResolving;
using Project.Enemies;
using Project.JobSystem;
using Project.Utilities.Extantions;
using XL1TTE.GameActions;


namespace Project.Cards.Effects{
    
    [Serializable]
    public class FireDamageEffect : ICardEffect
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
            
            var anim = new ColorWithTextCardAnimation("#ff9115".ToColor(), "Fire Damage!").GetAnimation(cardView);

            JobSequence job = new JobSequence(new List<Job>{
                new JobApplyCardEffects(target, 25),
                anim,
            });
            
            return job;
        }
    }
}
