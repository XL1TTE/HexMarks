using System.Collections;
using UnityEngine.UIElements;

namespace Project.DataResolving{
    
    
    public interface IDataRequestResolver{
        
        bool CanResolve(DataRequierment req);
        object Resolve(DataRequierment req);
    }
}
