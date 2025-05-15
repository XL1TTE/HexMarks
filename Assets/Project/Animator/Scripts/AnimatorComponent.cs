using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XL1TTE.Animator{
    internal class AnimatorComponent: MonoBehaviour{
        private Dictionary<xlAnimation, AwaitableRoutine> m_activeAnimations = new();
        private int m_buffer = 128;

        void OnDestroy()
        {
            foreach(var pair in m_activeAnimations){
                pair.Value.Kill();
            }
            m_activeAnimations = null;
        }

        internal void OnKillAnimation(xlAnimation animation){
            if(m_activeAnimations.ContainsKey(animation)){
                
                m_activeAnimations[animation].Kill();
                
                m_activeAnimations.Remove(animation);
            }
            
        }
        
        internal void OnPlayAnimation(xlAnimation animation){
            
            if(m_activeAnimations.ContainsKey(animation)){
                Debug.LogWarning("You are truing to start the same animation more then once!");
                return;
            }
            
            if(m_activeAnimations.Count >= m_buffer){
                Debug.LogWarning("Animations buffer is full!");
                return;
            }

            var routine = new AwaitableRoutine(this, animation.GetAnimation());
            m_activeAnimations.Add(animation, routine);
            
            StartCoroutine(CleanUpRoutine(animation, routine));
        }

        internal bool isAnimationCompleted(xlAnimation animation)
        {
            if (!m_activeAnimations.ContainsKey(animation)) { return true; }

            return m_activeAnimations[animation].IsDone;
        }
        
        IEnumerator CleanUpRoutine(xlAnimation animation, AwaitableRoutine routine){
            if(animation == null){yield break;}
            
            yield return animation.WaitForCompletion();
            m_activeAnimations.Remove(animation);
        }
    }
    
}
