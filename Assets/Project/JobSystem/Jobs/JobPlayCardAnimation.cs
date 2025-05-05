
using System.Collections;
using DG.Tweening;

namespace Project.JobSystem{

    public class JobPlayAnimation : Job
    {
        public JobPlayAnimation(Sequence a_animation)
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
}
