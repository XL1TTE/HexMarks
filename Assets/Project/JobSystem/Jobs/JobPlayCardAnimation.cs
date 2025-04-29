
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Project.JobSystem{

    public class JobPlayCardAnimation : Job
    {
        public JobPlayCardAnimation(Sequence a_animation)
        {
            m_animation = a_animation;
        }
        
        Sequence m_animation;        
        public override IEnumerator Proccess()
        {
            yield return PlayAnimation(m_animation);
        }

        private IEnumerator PlayAnimation(Sequence animation){
            animation.SetAutoKill(false);

            yield return animation.Play();

            yield return animation.WaitForCompletion();

            animation.Kill();
        }
    }

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
