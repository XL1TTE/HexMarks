
using System.Collections;
using Project.Cards;
using Project.Factories;

namespace Project.JobSystem{
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
