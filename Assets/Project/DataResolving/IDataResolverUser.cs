using System.Collections.Generic;

namespace Project.DataResolving{
    public interface IDataResolverUser{
        IReadOnlyList<DataRequierment> GetDataRequests();
    }


}
