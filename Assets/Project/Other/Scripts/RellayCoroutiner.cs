using System.Collections;
using UnityEngine;

namespace Project.Other{
    
    public class RellayCoroutiner: MonoBehaviour{
        
        private static RellayCoroutiner m_current;

        void Awake()
        {
            if(m_current == null){
                m_current = this;
            }
        }

        public static Coroutine StartRellayCoroutine(IEnumerator routine){
            return m_current.StartCoroutine(routine);
        }
        
        public static void StopAllRellayCoroutines(){
            m_current.StopAllCoroutines();
        }
        
        public static void StopRellayCoroutine(Coroutine coroutine){
            m_current.StopCoroutine(coroutine);
        }
        
    }
    
}
