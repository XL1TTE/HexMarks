using DG.Tweening;
using Project.Enemies;
using Project.JobSystem;
using Project.Utilities.Tooltips;
using TMPro;
using UnityEngine;

namespace Project.Cards.Effects{
    public class ColorWithTextEnemyAnimation{
        public ColorWithTextEnemyAnimation(Color defaultColor, Color targetColor, string text)
        {
            m_defaultColor = defaultColor;
            m_targetColor = targetColor;
            m_text = text;
        }

        private Color m_defaultColor;
        private Color m_targetColor;
        private string m_text;

        public Job GetAnimation(EnemyView enemyView)
        {
            var m_animSequence = DOTween.Sequence();
            TMP_Text text = null;

            m_animSequence.AppendCallback(() =>
            {
                text = FloatingTextUtility.ShowWorldText(
                    m_text,
                    enemyView.transform.position + Vector3.up * (enemyView.GetRenderer().bounds.extents.y + 0.3f),
                    enemyView.transform,
                    6,
                    m_targetColor
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
                enemyView.GetRenderer().DOColor(m_targetColor, 0.5f)        
            );

            m_animSequence.Join(enemyView.transform.DOShakePosition(
                    1f,
                    new Vector3(0.15f, 0.15f, 0),
                    vibrato: 45,
                    randomnessMode: ShakeRandomnessMode.Harmonic
            ));

            m_animSequence.Append(
                enemyView.GetRenderer().DOColor(m_defaultColor, 0.5f)
            );

            m_animSequence.AppendCallback(() =>
            {
                if (text != null)
                    FloatingTextUtility.HideWorldText(text);
                textPunchTween.Kill(false);
            });

            return new JobPlayAnimation(m_animSequence);
        }

    }
}
