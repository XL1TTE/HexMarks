
using System;
using System.Collections;
using System.Collections.Generic;
using Project.Actors;
using Project.Cards;
using Project.Cards.Effects;
using Project.Enemies;
using Project.JobSystem;
using UnityEngine;
using XL1TTE.GameActions;

namespace XL1TTE.GameAbilities.CardActions{
    
    [Serializable]
    public class BloodHarvest : CardAction
    {   
        [SerializeField] ParticleSystem m_OnEnemiesEffect;
        [SerializeField] ParticleSystem m_OnHeroHeal;
        
        
        [Header("Effect settings")]
        [SerializeField, Range(0, 10)] float m_Damage;
        [SerializeField, Range(0.1f, 2f)] float m_HealFactor;
        
        public override IEnumerable<DataRequest> GetRequests()
        {
            return new List<DataRequest>{
              new DataRequest("EnemiesInBattle", typeof(List<EnemyView>)),
            };
        }

        public override IEnumerator Execute(Card card, Context context)
        {
            var Enemies = context.Get<List<EnemyView>>("EnemiesInBattle");

            var enemiesHitAnim = new ColorWithTextEnemyAnimation(Color.white, Color.red, "Harvest!");

            var bloodHarvestJob = new List<Job>();

            IEnumerator HarvestBlood(EnemyView enemy, Hero caster){
                var damageTaken = enemy.TakeDamage(m_Damage);
                
                caster.TakeHeal(damageTaken * m_HealFactor);
                yield break;
            }            

            Hero caster = card.m_state.GetOwner();

            foreach (var enemy in Enemies)
            {
                bloodHarvestJob.Add(enemiesHitAnim.GetAnimation(enemy));
                bloodHarvestJob.Add(new JobPlayRoutine(HarvestBlood(enemy, caster)));
            }

            return new ParallelJobSequence(bloodHarvestJob, card.m_view).Proccess();

        }

    }
}
