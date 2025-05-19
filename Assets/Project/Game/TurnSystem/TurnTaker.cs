using Project.Actors;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;

namespace Project.TurnSystem{
    public abstract class ITurnTaker{
        
        protected ITurnTaker(SignalBus signalBus){
            m_SignalBus = signalBus;
            
        }
        protected SignalBus m_SignalBus;
        public abstract void SendTurnNotification();
        public abstract int GetInitiative();
    }


    public class HeroTurnTaker : ITurnTaker
    {

        public HeroTurnTaker(SignalBus signalBus, Hero hero) : base(signalBus)
        {
            m_hero = hero;
        }

        private Hero m_hero;
        public Hero GetHero() => m_hero;
        
        public override int GetInitiative()
        {
            return m_hero.GetInitiative();
        }

        public override void SendTurnNotification()
        {
            m_SignalBus.SendSignal(new HeroTurnSignal(m_hero));
        }
    }

    public class EnemyTurnTaker : ITurnTaker
    {
        private EnemyView m_enemy;
        public EnemyView GetEnemy() => m_enemy;

        public EnemyTurnTaker(SignalBus signalBus, EnemyView enemy) : base(signalBus)
        {
            m_enemy = enemy;
        }

        public override int GetInitiative()
        {
            return m_enemy.GetState().GetInitiaive();
        }

        public override void SendTurnNotification()
        {
            m_SignalBus.SendSignal(new EnemyTurnSignal(m_enemy));
        }
    }
}
