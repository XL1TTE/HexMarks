using Project.Actors;

namespace Project.EventBus.Signals{
    public class RequestDrawCardsSignal: ISignal{
        public RequestDrawCardsSignal(HeroView hero, int amount, bool clearHand = false){
            Amount = amount;
            ClearHand = clearHand;
            Hero = hero;
        }
        public int Amount;
        public bool ClearHand;
        public HeroView Hero;
    }
    
    public class RequestCardsDraggingStateSwitchSignal: ISignal{
        public RequestCardsDraggingStateSwitchSignal(bool isEnabled){
            this.isEnabled = isEnabled;
        }
        public bool isEnabled;
    }
}
