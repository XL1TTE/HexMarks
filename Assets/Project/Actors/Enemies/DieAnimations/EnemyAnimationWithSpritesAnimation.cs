using System;
using System.Collections;
using UnityEngine;

namespace Project.Enemies{
    
    [Serializable]
    public class EnemyAnimationWithSpritesAnimation : BaseEnemyAnimation
    {
        [SerializeField] private Sprite[] m_Sprites;
        [SerializeField, Range(50.0f, 1000.0f)] private float m_FrameSpeed = 100.0f; 
        public override IEnumerator GetAnimationRoutine(EnemyView enemyView)
        {
            foreach(var sprite in m_Sprites){
                enemyView.GetRenderer().sprite = sprite;
                yield return new WaitForSeconds(m_FrameSpeed / 1000.0f);
            }
            
        }
    }
}
