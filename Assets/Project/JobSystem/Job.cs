using System.Collections;
using System.Collections.Generic;

namespace Project.JobSystem{
    
    
    public abstract class Job{
        public abstract IEnumerator Proccess();
    }
    
    public class JobSequence : Job{
        public JobSequence(List<Job> a_Jobs){
            m_Jobs = a_Jobs;
        }
        private List<Job> m_Jobs;
        
        public override IEnumerator Proccess()
        {
            foreach(var job in m_Jobs){
                yield return job.Proccess();
            }
        }
    }
}
