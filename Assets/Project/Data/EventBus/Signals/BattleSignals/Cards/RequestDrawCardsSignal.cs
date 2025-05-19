using Project.Actors;

namespace Project.EventBus.Signals{
    public class RequestDrawCardsSignal: ISignal{
        public RequestDrawCardsSignal(Hero hero, HeroDeck deck, int amount, bool clearHand = false){
            Amount = amount;
            ClearHand = clearHand;
            Hero = hero;
            Deck = deck;
        }
        public int Amount;
        public bool ClearHand;
        public Hero Hero;
        public HeroDeck Deck;
    }
    
    public class RequestCardsDraggingStateSwitchSignal: ISignal{
        public RequestCardsDraggingStateSwitchSignal(bool isEnabled){
            this.isEnabled = isEnabled;
        }
        public bool isEnabled;
    }
}
