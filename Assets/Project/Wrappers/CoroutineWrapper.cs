using System.Collections;
using UnityEngine;

namespace Project.Wrappers{
    public class AwaitedCoroutine
    {
        public Coroutine Coroutine { get; private set; }
        public bool IsDone { get; private set; } = false;

        public AwaitedCoroutine(MonoBehaviour owner, IEnumerator routine)
        {
            Coroutine = owner.StartCoroutine(Run(routine));
        }

        private IEnumerator Run(IEnumerator routine)
        {
            yield return routine;
            IsDone = true;
        }
    }
}
