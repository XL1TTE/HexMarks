using Project.Actors;

namespace Project.EventBus.Signals{
    
    
    public class HeroSpawnedSignal: ISignal{
        
        public HeroSpawnedSignal(HeroView heroView){
            m_hero = heroView;
        }
        
        private HeroView m_hero;
        public HeroView GetHero() => m_hero;
        
    }
    public class HeroDiedSignal: ISignal{
        
        public HeroDiedSignal(HeroView heroView){
            m_hero = heroView;
        }
        
        private HeroView m_hero;
        public HeroView GetHero() => m_hero;
        
    }
    public class HeroHealthChangedSignal: ISignal{
        
        public HeroHealthChangedSignal(HeroView heroView){
            m_hero = heroView;
        }
        
        private HeroView m_hero;
        public HeroView GetHero() => m_hero;
        
    }
}
