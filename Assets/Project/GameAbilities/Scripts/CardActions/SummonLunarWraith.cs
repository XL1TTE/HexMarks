
using System;
using System.Collections;
using System.Collections.Generic;
using GameUtilities;
using Project.Cards;
using Project.Enemies;
using Project.Enemies.Abilities;
using Project.Game.Battle;
using Project.Other;
using UnityEngine;
using XL1TTE.Animator;
using XL1TTE.GameActions;

namespace XL1TTE.GameAbilities.CardActions{

    [Serializable]
    public class SummonLunarWraith : CardAction, IContextResolverUser
    {        
        [SerializeField] int m_effectTriggerFrameIndex = 0;
        
        [SerializeField] LunarWrathScript m_LunarWraithPrefab;
        
        
        public IEnumerable<DataRequest> GetRequests()
        {
            return new List<DataRequest>{
                new DataRequest("EnemyTarget", typeof(EnemyView)),
                new DataRequest("LastAllyCardPlayed", typeof(CardView)),
            };
        }

        public override IEnumerator Execute(CardView card)
        {
            Context context = ContextResolver.Resolve(this);
            
            CardView lastPlayedCard = context.Get<CardView>("LastAllyCardPlayed");
            EnemyView enemyTarget = context.Get<EnemyView>("EnemyTarget");
            
            if(lastPlayedCard == null){ Debug.Log("Can't use this card."); yield break; }


            /* ########################################## */
            /*            Played Card Animation           */
            /* ########################################## */
            
            lastPlayedCard.transform.position = Enviroment.TOP_CENTER_OUT_OF_SCREEN.position;

            /* ########################################## */
            /*           Lunar Wraith Animation           */
            /* ########################################## */
            
            var lunarWrath = UnityEngine.Object.Instantiate(m_LunarWraithPrefab, Enviroment.LEFT_TOP_OUT_OF_SCREEN.position, m_LunarWraithPrefab.transform.rotation);
            
            var target_pos = enemyTarget.transform.position + new Vector3(-4f, 0f, 0f);
            
            yield return GameUtility.FlyToPosition(lunarWrath.gameObject, target_pos, 3f);

            var wrath_cast = lunarWrath.GetCastAnimation();

            IEnumerator ApplyEffectCoroutine(){
                lastPlayedCard.gameObject.SetActive(true);
                yield return GameUtility.FlyToCenterOfScreen(lastPlayedCard.gameObject, 2f);
                lastPlayedCard.PlayCard();
            }
            
            void ApplyEffect(){
                RellayCoroutiner.StartRellayCoroutine(ApplyEffectCoroutine());
            }

            wrath_cast.AddFrameCallback(m_effectTriggerFrameIndex, () => ApplyEffect());

            lunarWrath.StopIdleAnimation();
            
            yield return wrath_cast.Play().WaitForCompletion();
            
            yield return lunarWrath.GetDisappearAnimation().Play().WaitForCompletion();

            lunarWrath.DestroyGameObject();
        }

    }

}
