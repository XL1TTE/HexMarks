using System.Collections.Generic;
using System.Data;
using DG.Tweening;
using Project.DataResolving;
using Project.JobSystem;
using Project.Utilities.Tooltips;
using TMPro;
using UnityEngine;

namespace Project.Cards.Effects{
    public class ColdDamageEffect : ICardEffect
    {
        public override IReadOnlyList<DataRequierment> GetDataRequests()
        {
            return new List<DataRequierment>{
                new DataRequierment("EnemyTarget", typeof(GameObject))  
            };
        }

        public override JobSequence GetJob(CardView cardView, DataContext context)
        {
            
            var target = context.Get<GameObject>("EnemyTarget");
            Debug.Log(target);
            
            var m_animSequence = DOTween.Sequence();

            TMP_Text text = null;

            m_animSequence.AppendCallback(() =>
            {
                text = FloatingTextUtility.ShowWorldText(
                    "Cold damage!",
                    cardView.transform.position + Vector3.up * (cardView.GetRenderer().bounds.extents.y + 0.3f),
                    cardView.transform,
                    6,
                    Color.blue
                );
            });


            // 2. Punching text.
            Tween textPunchTween = null;
            m_animSequence.AppendCallback(() =>
            {
                if (text != null)
                {
                    var originalScale = text.transform.localScale;

                    textPunchTween = text.transform.DOPunchScale(
                        originalScale * 0.2f,
                        0.5f,
                        vibrato: 0
                    );
                }
            });

            // 3. Card color pulse.
            m_animSequence.Join(
                cardView.GetRenderer().DOColor(Color.blue, 0.25f).SetLoops(2, LoopType.Yoyo)
            );

            // 4. Card Shake effect.
            m_animSequence.Join(cardView.GetTransform().DOShakePosition(
                    0.5f,
                    new Vector3(0.15f, 0.15f, 0),
                    vibrato: 45,
                    randomnessMode: ShakeRandomnessMode.Harmonic
            ));


            m_animSequence.AppendCallback(() =>
            {
                if (text != null)
                    FloatingTextUtility.HideWorldText(text);
                    textPunchTween.Kill(true);
            });

            JobSequence job = new JobSequence(new List<Job>{
                new JobPlayCardAnimation(m_animSequence),
                new JobApplyCardEffects()
            });   
            
            return job;                   
        }
    }
}
