using System;
using System.Collections;
using System.Collections.Generic;
using Project.Cards;
using Project.Cards.Effects;
using Project.Enemies;
using Project.JobSystem;
using Project.Sound;
using Project.Utilities.Extantions;
using UnityEngine;
using XL1TTE.GameActions;

namespace XL1TTE.GameAbilities.CardActions{
    
    [Serializable]
    public class FireBall : CardAction, IContextResolverUser
    {
        [SerializeField] AudioClip FireBall_SFX;
        [SerializeField] SoundChannel SFX_Channel;
        
        public IEnumerable<DataRequest> GetRequests()
        {
            return new List<DataRequest>{
                new DataRequest("EnemiesInBattle", typeof(List<EnemyView>)),
                //new DataRequierment("CardCaster", typeof(PlayerInBattle))
            };
        }

        public override IEnumerator Execute(CardView card)
        {
            var context = ContextResolver.Resolve(this);
            
            var Enemies = context.Get<List<EnemyView>>("EnemiesInBattle");
            //var CardCaster = context.Get<PlayerInBattle>("CardCaster");

            var EnemyBurnAnim = new ColorWithTextEnemyAnimation(Color.white, "#ff9115".ToColor(), "Burning!");

            var fireball_effects = new List<Job>();

            foreach (var enemy in Enemies){
                fireball_effects.Add(EnemyBurnAnim.GetAnimation(enemy));
                fireball_effects.Add(new JobApplyCardEffects(enemy, 25));
            }
            
            return new JobSequence(new List<Job>{
                new JobPlaySound(FireBall_SFX, SFX_Channel),
                new ParallelJobSequence(fireball_effects, card),     
            }).Proccess();
        }
    }
}
