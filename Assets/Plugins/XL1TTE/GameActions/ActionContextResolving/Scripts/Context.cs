using System;
using System.Collections.Generic;

namespace XL1TTE.GameActions{
    public class Context{
        Dictionary<string, object> context = new();
        public T Get<T>(string key){
            context.TryGetValue(key, out var value);
            if(value is T obj){return obj;}
            
            else if(value == null){
                return default;
            }
            
            else{
                throw new Exception($"Unable to find record with key: {key} of type {typeof(T)}");
            }
        }
        public void Set(string key, object obj){
            context.TryAdd(key, obj);
        }
    }
    
}
