using Project.Player;
using Zenject;

namespace Project.DataResolving.DataRequestResolvers{
    public class PlayerDataResolver : IDataRequestResolver
    {
        [Inject]
        private void Construct(PlayerData playerData){
            m_PlayerData = playerData;
        }
        private PlayerData m_PlayerData;
        
        public bool CanResolve(DataRequierment req)
        {
            return req.GetReqName() == "Player" && req.GetReqDataType() == typeof(PlayerData);
        }

        public object Resolve(DataRequierment req)
        {
            return m_PlayerData;
        }
    }
}
