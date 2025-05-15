using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using Project.Actors;
using Project.Data.CMS.Tags.Generic;
using Project.DataResolving;
using Project.Utilities.Extantions;
using UnityEngine;
using XL1TTE.Animator;

namespace Project.Enemies.AI{
    
    [Serializable]
    public abstract class BaseEnemyAction : IDataResolverUser
    {
        public abstract IReadOnlyList<DataRequierment> GetDataRequests();
        public abstract IEnumerator GetAction(EnemyView state, DataContext context);
    }

    [Serializable]
    public class AttackLowestHealthHero : BaseEnemyAction
    {
        [SerializeField] EnemyAnimationWithSpritesAnimation m_AttackAnimation;
        
        [SerializeField] int AttackFrameIndex;
        
        public override IReadOnlyList<DataRequierment> GetDataRequests()
        {
            return new List<DataRequierment>{
                new DataRequierment("HeroesInBattle", typeof(List<HeroView>))
            };
        }

        public override IEnumerator GetAction(EnemyView state, DataContext context)
        {
            var all_heroes = context.Get<List<HeroView>>("HeroesInBattle");

            if(all_heroes.IsEmpty()){yield break;}

            var enemy = state.GetController();
            
            HeroView toAttack = all_heroes[0];
            
            foreach(var hero in all_heroes){
                if(hero.GetState().m_stats.m_BaseStats.m_Health < toAttack.GetState().m_stats.m_BaseStats.m_Health){
                    toAttack = hero;
                }
            }

            toAttack.GetState().m_model.Is<TagName>(out var tagName);

            Debug.Log($"Damaging hero: {tagName.name} with: {enemy.GetEnemyDamage()} damage.");
            

            state.StopIdleAnimation();

            // plays attack animation
            var anim = m_AttackAnimation.GetAnimation(state) as FrameAnimation;
            
            anim.AddFrameCallback(AttackFrameIndex, () => {
                toAttack.TakeDamage(enemy.GetEnemyDamage());
            });
            
            yield return anim.Play().WaitForCompletion();

            state.StartIdleAnimation();
        }
    }
    

}
