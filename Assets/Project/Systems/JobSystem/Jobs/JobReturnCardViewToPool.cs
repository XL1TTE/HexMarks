
using System.Collections;
using Project.Cards;
using Project.Factories;

namespace Project.JobSystem{
    public class JobReturnCardViewToPool : Job
    {
        public JobReturnCardViewToPool(CardView a_card){
            m_card = a_card;
        }
        CardView m_card;
        public override IEnumerator Proccess()
        {
            CardViewObjectPool.current.Return(m_card);
            yield break;
        }
    }
}
