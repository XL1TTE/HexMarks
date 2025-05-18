using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace XL1TTE.GameActions{
    public class ContextResolver{
        private static ContextResolver current;
        
        public static void Load(ContextResolver resolver){
            if(current == null){
                current = resolver;
            }
        }
        public static void Unload(){
            current.m_resolvers = null;
            current = null;
        }
        
        public ContextResolver(IEnumerable<IDataRequestResolver> resolvers){
            this.m_resolvers = resolvers.ToList();
        }
        
        public ContextResolver(List<IDataRequestResolver> resolvers){
            this.m_resolvers = resolvers;
        }
        
        private List<IDataRequestResolver> m_resolvers = new();
        
        public void AddDataResolver(IDataRequestResolver resolver){
            m_resolvers.Add(resolver);
        }
        public void RemoveDataResolver(IDataRequestResolver resolver){
            if(m_resolvers.Contains(resolver)){m_resolvers.Remove(resolver);}
        }
    
        public static Context Resolve(IContextResolverUser subject){
            Context context = new Context();
            
            foreach(var req in subject.GetRequests()){
                var resolver = current.m_resolvers.FirstOrDefault((r) => r.CanResolve(req));
                if(resolver == null){
                    throw new Exception($"None of registred resolvers was able to resolve request (reqKey: {req.Key}, reqType: {req.Type})");
                }
                
                context.Set(req.Key, resolver.Resolve(req));
            }
            return context;
        }
    }
}
