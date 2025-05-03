
using System.Collections;
using System.IO;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Player;

namespace Project.TurnSystem{
    public abstract class ITurnTaker{
        
        protected ITurnTaker(SignalBus signalBus){
            m_SignalBus = signalBus;
        }
        protected SignalBus m_SignalBus;
        public abstract void SendTurnNotification();
    }


    public class PlayerTurnTaker : ITurnTaker
    {
        private PlayerData m_player;

        public PlayerTurnTaker(SignalBus signalBus, PlayerData playerData) : base(signalBus)
        {
            m_player = playerData;
        }

        public override void SendTurnNotification()
        {
            m_SignalBus.SendSignal(new PlayerTurnSignal(m_player));
        }
    }

    public class EnemyTurnTaker : ITurnTaker
    {
        private EnemyView m_enemy;

        public EnemyTurnTaker(SignalBus signalBus, EnemyView enemy) : base(signalBus)
        {
            m_enemy = enemy;
        }

        public override void SendTurnNotification()
        {
            m_SignalBus.SendSignal(new EnemyTurnSignal(m_enemy));
        }
    }
    
    public interface IHaveInitiative{
        float GetInitiaive();
    }
}
