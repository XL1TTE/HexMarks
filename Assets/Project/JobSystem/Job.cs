using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Wrappers;
using UnityEngine;

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

    public class ParallelJobSequence : Job
    {
        public ParallelJobSequence(List<Job> a_Jobs, MonoBehaviour a_routinesParent)
        {
            m_Jobs = a_Jobs;
            m_RoutinesParent = a_routinesParent;
        }
        private List<Job> m_Jobs;
        private MonoBehaviour m_RoutinesParent;
        
        List<AwaitedCoroutine> m_ExecutingJobs = new();
        
        public override IEnumerator Proccess()
        {
            foreach (var job in m_Jobs)
            {
                m_ExecutingJobs.Add(new AwaitedCoroutine(m_RoutinesParent, job.Proccess()));
            }
            
            while(m_ExecutingJobs.Any(j => !j.IsDone)){
                yield return null;
            }
        }
    }
}
