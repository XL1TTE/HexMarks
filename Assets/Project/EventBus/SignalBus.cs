using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using Project.JobSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.EventBus{
    
    public delegate IEnumerator AwaitableAction<T>(T signal);

    
    public class SignalBus : MonoBehaviour{
        
        Dictionary<string, List<object>> m_Subscribers = new();
                        
        public void Subscribe<T>(AwaitableAction<T> subscriber) where T: ISignal
        {
            string key = typeof(T).FullName;
            if (!m_Subscribers.ContainsKey(key)){
                m_Subscribers.Add(key, new List<object>{subscriber});
                return;
            }
            m_Subscribers[key].Add(subscriber);
        }   
        
        public void Unsubscribe<T>(AwaitableAction<T> subscriber) where T: ISignal
        {
            string key = typeof(T).FullName;
            if(!m_Subscribers.TryGetValue(key, out var subscribers)){return;}
            subscribers.Remove(subscriber);
        }
        
        public void SendSignal<T>(T signal) where T: ISignal
        {
            string key = signal.GetType().FullName;
            if(!m_Subscribers.TryGetValue(key, out var subscribers)){return;}
            
            List<Job> invokationList = new();

            foreach (var item in subscribers){
                if(item is AwaitableAction<T> sub){;
                    invokationList.Add(new JobPlayRoutine(sub?.Invoke(signal)));
                }
            }
            StartCoroutine(new JobSequence(invokationList).Proccess());
        }
        
        
    }
    
}
