using System.Collections.Generic;
using Project.DataResolving.DataRequestResolvers;
using Zenject;

namespace Project.DataResolving{
    public class DataRosolver{
        private List<IDataRequestResolver> m_reqResolvers = new();
        
        [Inject]
        private void Construct(
            EnemyTargetResolver res1, 
            PlayerInBattleReqResolver res2,
            EnemiesInBattleReqResolver res3)
        {
            // Adding reqResolvers here;
            m_reqResolvers.Add(res1);
            m_reqResolvers.Add(res2);
            m_reqResolvers.Add(res3);
        }
        
        public DataContext Resolve(IDataResolverUser user){
            
            DataContext resolved = new DataContext();
            
            foreach(var req in user.GetDataRequests()){
                foreach(var res in m_reqResolvers){
                    if(res.CanResolve(req)){
                        resolved.Set(req.GetReqName(), res.Resolve(req));
                    }
                }
            }
            
            return resolved;
        }
    }

}
