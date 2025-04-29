
using System;

namespace Project.DataResolving{
    public class DataRequierment{
        public DataRequierment(string reqName, Type reqDataType){
            m_ReqName = reqName;
            m_ReqDataType = reqDataType;
        }
        private string m_ReqName;
        public string GetReqName() => m_ReqName;
        private Type m_ReqDataType;
        public Type GetReqDataType() => m_ReqDataType;
        
    }

}
