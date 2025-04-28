using System.Collections;
using Project.JobSystem;

namespace Project.Cards{
    public class Card
    {
        public Card(CardModel a_model, CardView a_cardView){
            m_model = a_model;
            m_view = a_cardView;
            m_view.Init(this);
        }
        
        private CardModel m_model;
        private CardView m_view;
        public CardView GetView() => m_view;
        
        public IEnumerator UseCard(){
            yield return m_model.GetCardExecutionSequence(m_view).Proccess();
            yield return new JobReturnCardViewToPool(m_view).Proccess();
        }
    }
}

