using System.Collections.Generic;
using DG.Tweening;
using Project.DataResolving;
using Project.Enemies;
using Project.JobSystem;
using Project.Utilities.Extantions;
using Project.Utilities.Tooltips;
using TMPro;
using UnityEngine;

namespace Project.Cards.Effects{
    public class FireDamageEffect : ICardEffect
    {
        public override IReadOnlyList<DataRequierment> GetDataRequests()
        {
            return new List<DataRequierment>{
              new DataRequierment("EnemyTarget", typeof(EnemyView))  
            };
        }

        public override JobSequence GetJob(CardView cardView, DataContext context)
        {
            var target = context.Get<EnemyView>("EnemyTarget");

            var m_animSequence = DOTween.Sequence();
            TMP_Text text = null;

            m_animSequence.AppendCallback(() =>
            {
                text = FloatingTextUtility.ShowWorldText(
                    "Fire damage!",
                    cardView.transform.position + Vector3.up * (cardView.GetRenderer().bounds.extents.y + 0.3f),
                    cardView.transform,
                    6,
                    "#ff9115".ToColor()
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


            m_animSequence.Append(
                cardView.GetRenderer().DOColor("#ff9115".ToColor(), 0.5f).SetLoops(2, LoopType.Yoyo)
            );

            m_animSequence.Join(cardView.GetTransform().DOShakePosition(
                    1f,
                    new Vector3(0.15f, 0.15f, 0),
                    vibrato: 45,
                    randomnessMode: ShakeRandomnessMode.Harmonic
            ));

            m_animSequence.AppendCallback(() =>
            {
                if (text != null)
                    FloatingTextUtility.HideWorldText(text);
                    textPunchTween.Kill(false);
            });

            JobSequence job = new JobSequence(new List<Job>{
                new JobApplyCardEffects(target, 25),
                new JobPlayCardAnimation(m_animSequence),
            });
            
            return job;
        }
    }
}
