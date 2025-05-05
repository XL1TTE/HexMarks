using Project.Player;

namespace Project.EventBus.Signals{
    public class PlayerTurnSignal: ISignal{
        private PlayerInBattle m_player;
        public PlayerInBattle GetPlayer() => m_player;
        public PlayerTurnSignal(PlayerInBattle player){
            m_player = player;
        }
    }
    
    public class PlayerTurnEndSignal: ISignal{
        private PlayerData m_player;
        public PlayerData GetPlayer() => m_player;
        public PlayerTurnEndSignal(PlayerData player){
            m_player = player;
        }
    }
    
}
