using System.Collections;
using Project.DataResolving;
using Project.JobSystem;

namespace Project.Cards{
    public class Card
    {
        public Card(CardModel a_model, CardView a_cardView, DataRosolver DataResolver){
            m_model = a_model;
            m_view = a_cardView;
            m_view.Init(this);
            
            m_dataResolver = DataResolver;
        }
        
        private DataRosolver m_dataResolver;
        private CardModel m_model;
        private CardView m_view;
        public CardView GetView() => m_view;
        
        public IEnumerator GetCardUseSequence(){
            yield return new JobSwitchColliderEnabledState(m_view.gameObject, false).Proccess();
            yield return m_model.GetCardExecutionSequence(m_view, m_dataResolver).Proccess();
            yield return new JobSwitchColliderEnabledState(m_view.gameObject, true).Proccess();
            
        }
    }
}

