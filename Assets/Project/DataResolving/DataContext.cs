
using System;
using System.Collections.Generic;

namespace Project.DataResolving{
    public class DataContext{
        private Dictionary<string, object> m_Data = new();
        
        public void Set<T>(string key, T data) where T: class{
            if(!m_Data.ContainsKey(key)){
                m_Data.Add(key, data);
            }
            m_Data[key] = data;
        }
        public T Get<T>(string key) where T: class{
            if(m_Data.ContainsKey(key)) {return m_Data[key] as T;}
            
            throw new Exception($"Unable to find record with key: {key}.");
        }
    }


}
