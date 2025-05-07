using System.Collections;
using UnityEngine;

namespace Project.Wrappers{
    public class AwaitedCoroutine
    {
        private Coroutine Coroutine { get; }
        private MonoBehaviour Owner {get;} 
        public bool IsDone { get; private set; } = false;

        public AwaitedCoroutine(MonoBehaviour owner, IEnumerator routine)
        {
            Coroutine = owner.StartCoroutine(Run(routine));
            Owner = owner;
        }

        private IEnumerator Run(IEnumerator routine)
        {
            yield return routine;
            IsDone = true;
            Kill();
        }
        
        public void Kill(){
            Owner.StopCoroutine(Coroutine);
        }
    }
}
