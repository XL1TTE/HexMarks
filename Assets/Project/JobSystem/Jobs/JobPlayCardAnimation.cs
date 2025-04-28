
using System.Collections;
using DG.Tweening;
using Project.Cards;
using Project.Factories;
using UnityEngine;

namespace Project.JobSystem{

    public class JobPlayCardAnimation : Job
    {
        public JobPlayCardAnimation(CardView a_cardView) => m_cardView = a_cardView;
        public JobPlayCardAnimation(Sequence a_animation, CardView a_cardView){
            m_animation = a_animation;
            m_cardView = a_cardView;
        }
        
        Sequence m_animation;
        CardView m_cardView;
        
        public override IEnumerator Proccess()
        {
            yield return PlayDefaultAnimation();
        }
        
        private IEnumerator PlayDefaultAnimation(){
            
            m_animation = DOTween.Sequence();

            m_animation.Append(
                m_cardView.GetRenderer().DOColor(Color.red, 0.25f).SetLoops(2, LoopType.Yoyo)
            );

            m_animation.Join(m_cardView.GetTransform().DOShakePosition(
                    0.5f,
                    new Vector3(0.15f, 0.15f, 0),
                    vibrato: 25
            ));

            m_animation.SetAutoKill(false);

            yield return m_animation.WaitForCompletion();


            m_animation.Kill();
        }
    }
    
    public class JobReturnCardViewToPool : Job
    {
        public JobReturnCardViewToPool(CardView a_cardView){
            m_cardView = a_cardView;
        }
        CardView m_cardView;
        public override IEnumerator Proccess()
        {
            CardViewObjectPool.current.Return(m_cardView);
            yield break;
        }
    }
}
