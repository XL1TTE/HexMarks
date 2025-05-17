using System.Collections.Generic;

namespace XL1TTE.GameActions{
    public interface IContextResolverUser{
        IEnumerable<DataRequest> GetRequests();
    }
    
}
