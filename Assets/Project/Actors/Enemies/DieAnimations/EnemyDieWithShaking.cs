using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Project.Enemies{
    
    [Serializable]
    public class EnemyDieWithShaking : BaseEnemyAnimation
    {
        [SerializeField] private Vector3 m_ShakePower = new Vector3(0.15f, 0.15f, 0);
        [SerializeField, Range(0, 5)] private float m_ShakeDuration = 2f;
        
        public override IEnumerator GetAnimationRoutine(EnemyView enemyView)
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
