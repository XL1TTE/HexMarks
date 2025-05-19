using Project.Actors;

namespace Project.EventBus.Signals{
    public class HeroTurnSignal: ISignal{
        public readonly Hero hero;
        public HeroTurnSignal(Hero hero){
            this.hero = hero;
        }
    }
}
