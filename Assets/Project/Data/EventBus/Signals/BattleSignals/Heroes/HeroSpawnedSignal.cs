using Project.Actors;

namespace Project.EventBus.Signals{
    
    
    public class HeroSpawnedSignal: ISignal{
        
        public HeroSpawnedSignal(Hero hero){
            this.hero = hero;
        }
        
        public readonly Hero hero;
        
    }
    public class HeroDiedSignal: ISignal{
        
        public HeroDiedSignal(Hero hero){
            this.hero = hero;
        }
        
        public readonly Hero hero;        
    }
    public class HeroHealthChangedSignal: ISignal{
        
        public HeroHealthChangedSignal(Hero hero){
            this.hero = hero;
        }
        
        public readonly Hero hero;        
    }

    public class HeroDamageTakenSignal: ISignal{
        public HeroDamageTakenSignal(Hero hero, float amount){
            this.hero = hero;
            this.amount = amount;
        }
        public readonly Hero hero;
        public readonly float amount;
    }
}
