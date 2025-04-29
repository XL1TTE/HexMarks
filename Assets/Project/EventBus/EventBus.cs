using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Project.EventBus{
    
    public class SignalBus{
        
        Dictionary<string, List<object>> m_Subscribers = new();
        
        public void Subscribe<T>(Action<T> subscriber) where T: ISignal
        {
            string key = typeof(T).FullName;
            if (!m_Subscribers.ContainsKey(key)){
                m_Subscribers.Add(key, new List<object>{subscriber});
                return;
            }
            m_Subscribers[key].Add(subscriber);
        }   
        
        public void Unsubscribe<T>(Action<T> subscriber) where T: ISignal
        {
            string key = typeof(T).FullName;
            if(!m_Subscribers.TryGetValue(key, out var subscribers)){return;}
            subscribers.Remove(subscriber);
        }
        
        public void SendSignal<T>(T signal) where T: ISignal
        {
            string key = typeof(T).FullName;
            if(!m_Subscribers.TryGetValue(key, out var subscribers)){return;}
            
            foreach(var sub in subscribers){
                Action<T> handler = sub as Action<T>;
                handler?.Invoke(signal);
            }
        }
        
    }
    
}
