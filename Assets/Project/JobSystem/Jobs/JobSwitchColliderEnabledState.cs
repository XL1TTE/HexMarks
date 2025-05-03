
using System.Collections;
using UnityEngine;

namespace Project.JobSystem{
    public class JobSwitchColliderEnabledState : Job
    {
        public JobSwitchColliderEnabledState(GameObject obj, bool IsEnabled){
            m_obj = obj;
            m_IsEnabled = IsEnabled;
        }
        private GameObject m_obj; 
        private bool m_IsEnabled;
        public override IEnumerator Proccess()
        {
            Collider2D collider;
            if(!m_obj.TryGetComponent(out collider)){
                
                collider = m_obj.GetComponentInChildren<Collider2D>();
                if (collider == null) { yield break; }
            }
            
            collider.enabled = m_IsEnabled;
        }
    }
}
