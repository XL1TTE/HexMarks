
using System.Collections;

namespace Project.JobSystem{
    public class JobPlayRoutine : Job
    {
        public JobPlayRoutine(IEnumerator routine){
            m_Routine = routine;
        }
        private IEnumerator m_Routine;
        
        public override IEnumerator Proccess()
        {
            yield return m_Routine;
        }
    }
}
