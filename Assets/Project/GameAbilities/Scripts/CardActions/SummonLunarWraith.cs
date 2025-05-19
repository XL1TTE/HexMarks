
using System;
using System.Collections;
using System.Collections.Generic;
using GameUtilities;
using Project.Cards;
using Project.Enemies;
using Project.Enemies.Abilities;
using Project.Game.Battle;
using Project.Other;
using Project.Sound;
using UnityEngine;
using XL1TTE.Animator;
using XL1TTE.GameActions;

namespace XL1TTE.GameAbilities.CardActions{

    [Serializable]
    public class SummonLunarWraith : CardAction
    {        
        [SerializeField] int m_effectTriggerFrameIndex = 0;
        
        [SerializeField] LunarWrathScript m_LunarWraithPrefab;
        
        [SerializeField] AudioClip m_LunarWrathSummonReplic;
        
        [SerializeField] SoundChannel m_SFXChannel;
        
        public override IEnumerable<DataRequest> GetRequests()
        {
            return new List<DataRequest>{
                new DataRequest("EnemyTarget", typeof(EnemyView)),
                new DataRequest("LastAllyCardPlayed", typeof(Card)),
            };
        }

        public override IEnumerator Execute(Card card, Context context)
        {            
            Card lastPlayedCard = context.Get<Card>("LastAllyCardPlayed");
            EnemyView enemyTarget = context.Get<EnemyView>("EnemyTarget");
            
            if(lastPlayedCard == null){ Debug.Log("Can't use this card."); yield break; }


            var caster = card.m_state.GetOwner();
            lastPlayedCard.m_state.SetOwner(caster);


            /* ########################################## */
            /*            Played Card Animation           */
            /* ########################################## */
            
            lastPlayedCard.m_view.transform.position = Enviroment.TOP_CENTER_OUT_OF_SCREEN.position;

            /* ########################################## */
            /*           Lunar Wraith Animation           */
            /* ########################################## */
            
            var lunarWrath = UnityEngine.Object.Instantiate(m_LunarWraithPrefab, Enviroment.LEFT_TOP_OUT_OF_SCREEN.position, m_LunarWraithPrefab.transform.rotation);
            
            
            var target_pos = enemyTarget.transform.position + new Vector3(-4f, 0f, 0f);

            m_SFXChannel.PlaySound(m_LunarWrathSummonReplic);

            yield return GameUtility.FlyToPosition(lunarWrath.gameObject, target_pos, 3f);

            var wrath_cast = lunarWrath.GetCastAnimation();

            IEnumerator ApplyEffectCoroutine(){
                lastPlayedCard.m_view.gameObject.SetActive(true);
                yield return GameUtility.FlyToCenterOfScreen(lastPlayedCard.m_view.gameObject, 2f);
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
