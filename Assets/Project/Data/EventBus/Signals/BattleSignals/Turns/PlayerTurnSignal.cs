using Project.Actors;

namespace Project.EventBus.Signals{
    public class HeroTurnSignal: ISignal{
        private HeroView m_hero;
        public HeroView GetHero() => m_hero;
        public HeroTurnSignal(HeroView hero){
            m_hero = hero;
        }
    }
}
