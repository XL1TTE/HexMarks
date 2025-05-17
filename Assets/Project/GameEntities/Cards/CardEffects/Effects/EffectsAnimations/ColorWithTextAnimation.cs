using DG.Tweening;
using Project.JobSystem;
using Project.Utilities.Extantions;
using Project.Utilities.Tooltips;
using TMPro;
using UnityEngine;

namespace Project.Cards.Effects{
    
    public class ColorWithTextCardAnimation{
        
        public ColorWithTextCardAnimation(Color color, string text){
            m_color = color;
            m_text = text;
        }
        
        private Color m_color;
        private string m_text;
        
        public Job GetAnimation(CardView cardView){
            var m_animSequence = DOTween.Sequence();
            TMP_Text text = null;

            m_animSequence.AppendCallback(() =>
            {
                text = FloatingTextUtility.ShowWorldText(
                    m_text,
                    cardView.transform.position + Vector3.up * (cardView.GetRenderer().bounds.extents.y + 0.3f),
                    cardView.transform,
                    6,
                    m_color
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
                cardView.GetRenderer().DOColor(m_color, 0.5f).SetLoops(2, LoopType.Yoyo)
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
            
            

            return new JobPlayAnimation(m_animSequence);
        }
        
    }
}
