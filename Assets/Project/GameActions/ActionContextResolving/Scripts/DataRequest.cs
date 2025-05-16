using System;

namespace XL1TTE.GameActions{
    public class DataRequest{
        public DataRequest(string key, Type type){
            Key = key;
            Type = type;
        }
        public string Key;
        public Type Type;
    }
    
}
