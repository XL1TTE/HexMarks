using System;
using System.Collections.Generic;
using Project.DataResolving;
using Project.Enemies;
using Project.JobSystem;
using Project.Utilities.Extantions;

namespace Project.Cards.Effects{
    
    [Serializable]
    public class AoeOnAllEnemiesFireDamageEffect : ICardEffect
    {
        public override IReadOnlyList<DataRequierment> GetDataRequests()
        {
            return new List<DataRequierment>{
                new DataRequierment("EnemiesInBattle", typeof(List<EnemyView>)),
                //new DataRequierment("CardCaster", typeof(PlayerInBattle))
            };
        }

        public override Job GetJob(CardView cardView, DataContext context)
        {
            var Enemies = context.Get<List<EnemyView>>("EnemiesInBattle");
            //var CardCaster = context.Get<PlayerInBattle>("CardCaster");

            var EnemyBurnAnim = new ColorWithTextEnemyAnimation("#ff9115".ToColor(), "Burning!");

            var job_seq = new List<Job>();

            foreach (var enemy in Enemies){
                job_seq.Add(EnemyBurnAnim.GetAnimation(enemy));
                job_seq.Add(new JobApplyCardEffects(enemy, 25));
            }
            
            return new ParallelJobSequence(job_seq, cardView);
        }
    }
}
