using Project.Player;
using Zenject;

namespace Project.DataResolving.DataRequestResolvers{
    public class PlayerInBattleReqResolver : IDataRequestResolver
    {
        private PlayerInBattle m_PlayerData;
        public void Set(PlayerInBattle state) => m_PlayerData = state;
        
        public bool CanResolve(DataRequierment req)
        {
            return req.GetReqName() == "PlayerInBattle" && req.GetReqDataType() == typeof(PlayerInBattle);
        }

        public object Resolve(DataRequierment req)
        {
            return m_PlayerData;
        }
    }
}
