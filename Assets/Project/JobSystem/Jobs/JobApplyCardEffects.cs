
using System.Collections;
using UnityEngine;

namespace Project.JobSystem{
    public class JobApplyCardEffects : Job
    {
        public JobApplyCardEffects(){}
        public override IEnumerator Proccess()
        {
            Debug.Log("Apply card effects");
            yield break;
        }
    }
}
