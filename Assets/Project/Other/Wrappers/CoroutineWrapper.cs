using System.Collections;
using UnityEngine;

namespace Project.Wrappers{
    public class AwaitableCoroutine
    {
        private Coroutine Coroutine { get; }
        private MonoBehaviour Owner {get;} 
        public bool IsDone { get; private set; } = false;

        public AwaitableCoroutine(MonoBehaviour owner, IEnumerator routine)
        {
            Coroutine = owner.StartCoroutine(Run(routine));
            Owner = owner;
        }

        private IEnumerator Run(IEnumerator routine)
        {
            yield return routine;
            IsDone = true;
        }
        
        public void Kill(){
            Owner.StopCoroutine(Coroutine);
        }
    }
}
