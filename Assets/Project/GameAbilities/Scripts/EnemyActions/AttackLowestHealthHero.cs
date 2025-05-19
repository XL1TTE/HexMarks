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
    public class AttackLowestHealthHero : EnemyAction
    {                
        public override IEnumerable<DataRequest> GetRequests()
        {
            return new List<DataRequest>{
                new DataRequest("HeroesInBattle", typeof(List<Hero>))
            };
        }

        public override IEnumerator Execute(EnemyView enemyView, Context context)
        {            
            var all_heroes = context.Get<List<Hero>>("HeroesInBattle");

            if(all_heroes.IsEmpty()){}

            var enemy = enemyView.GetState();
            
            Hero toAttack = all_heroes[0];
            
            foreach(var hero in all_heroes){
                if(hero.GetCurrentHealth() < toAttack.GetCurrentHealth()){
                    toAttack = hero;
                }
            }

            toAttack.GetModel().Is<TagName>(out var tagName);

            Debug.Log($"Damaging hero: {tagName.name} with: {enemy.GetEnemyDamage()} damage.");
            
            toAttack.TakeDamage(enemy.GetEnemyDamage());
            
            yield break;
        }
    }
    

}
