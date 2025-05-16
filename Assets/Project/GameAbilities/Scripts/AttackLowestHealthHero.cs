using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using Project.Actors;
using Project.Data.CMS.Tags.Generic;
using UnityEngine;
using XL1TTE.Animator;
using XL1TTE.GameActions;

namespace Project.Enemies.AI{

    [Serializable]
    public class AttackLowestHealthHero : EnemyAction, IContextResolverUser
    {                
        public IEnumerable<DataRequest> GetRequests()
        {
            return new List<DataRequest>{
                new DataRequest("HeroesInBattle", typeof(List<HeroView>))
            };
        }

        public override IEnumerator Execute(EnemyView state)
        {
            var context = ContextResolver.Resolve(this);
            
            var all_heroes = context.Get<List<HeroView>>("HeroesInBattle");

            if(all_heroes.IsEmpty()){}

            var enemy = state.GetController();
            
            HeroView toAttack = all_heroes[0];
            
            foreach(var hero in all_heroes){
                if(hero.GetState().m_stats.m_BaseStats.m_Health < toAttack.GetState().m_stats.m_BaseStats.m_Health){
                    toAttack = hero;
                }
            }

            toAttack.GetState().m_model.Is<TagName>(out var tagName);

            Debug.Log($"Damaging hero: {tagName.name} with: {enemy.GetEnemyDamage()} damage.");
            
            toAttack.TakeDamage(enemy.GetEnemyDamage());
            
            yield break;
        }
    }
    

}
