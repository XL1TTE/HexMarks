
using System;
using System.Collections;
using System.Collections.Generic;
using GameUtilities;
using Project.Cards;
using UnityEngine;
using XL1TTE.Animator;
using XL1TTE.GameActions;

namespace XL1TTE.GameAbilities.CardActions{

    [Serializable]
    public class SummonLunarWraith : CardAction, IContextResolverUser
    {
        [Header("Animation Settings")]
        [SerializeField] List<Sprite> m_LunarWraithAnimation = new();
        
        [SerializeField] int m_effectTriggerFrameIndex = 0;
        
        [SerializeField] SpriteRenderer m_LunarWraithObject = new();
        
        public IEnumerable<DataRequest> GetRequests()
        {
            return new List<DataRequest>{
                new DataRequest("LastAllyCardPlayed", typeof(CardView)),
            };
        }

        public override IEnumerator Execute(CardView card)
        {
            Context context = ContextResolver.Resolve(this);
            
            CardView lastPlayedCard = context.Get<CardView>("LastAllyCardPlayed");
            
            if(lastPlayedCard == null){ yield break; }
            
            
            /* ########################################## */
            /*            Played Card Animation           */
            /* ########################################## */
            
            // 
            
            
            /* ########################################## */
            /*           Lunar Wraith Animation           */
            /* ########################################## */
            
            var anim_wraith = m_LunarWraithObject.ToFrameAnimation(m_LunarWraithAnimation, 100);

            IEnumerator ApplyEffect(){
                yield return GameUtility.FlyToCenterOfScreen(lastPlayedCard.gameObject, 1f);
                yield return lastPlayedCard.OnCardPlayed().Proccess();
            }
            
            anim_wraith.AddFrameCallback(m_effectTriggerFrameIndex, () => ApplyEffect());
            
            
            
            yield return anim_wraith.Play().WaitForCompletion();
        }

    }

}
