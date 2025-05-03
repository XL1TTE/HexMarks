using System;
using System.Collections;
using UnityEngine;

namespace Project.Enemies{
    
    [CreateAssetMenu(fileName = "DieAnimationClip", menuName = "Enemies/DieAnims/DieAnimationClip")]
    public class EnemyDieAnimationClip : BaseEnemyDieAnimation
    {
        [SerializeField] private Animation m_animation; 
        public override IEnumerator GetDieSequence(EnemyView enemyView)
        {
            m_animation.Play();
            
            while(m_animation.isPlaying){
                yield return null;
            }
        }
    }
}
