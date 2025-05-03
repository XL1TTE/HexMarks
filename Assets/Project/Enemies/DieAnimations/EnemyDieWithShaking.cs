using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Project.Enemies{
    [CreateAssetMenu(fileName = "DieAnimationClip", menuName = "Enemies/DieAnims/DieWithShaking")]
    public class EnemyDieWithShaking : BaseEnemyDieAnimation
    {
        [SerializeField] private Vector3 m_ShakePower = new Vector3(0.15f, 0.15f, 0);
        [SerializeField, Range(0, 5)] private float m_ShakeDuration = 2f;
        
        public override IEnumerator GetDieSequence(EnemyView enemyView)
        {
            Tween animTween = enemyView.transform.DOShakePosition(
                    m_ShakeDuration,
                    m_ShakePower,
                    vibrato: 45,
                    randomnessMode: ShakeRandomnessMode.Harmonic
            );
            
            yield return animTween.WaitForCompletion();
            
            animTween.Kill();
        }
    }
}
