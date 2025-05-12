namespace Project.EventBus.Signals{
    public class RequestDrawCardsSignal: ISignal{
        public RequestDrawCardsSignal(int amount, bool clearHand = false){
            Amount = amount;
            ClearHand = clearHand;
        }
        public int Amount;
        public bool ClearHand;
    }
    
    public class RequestCardsDraggingStateSwitchSignal: ISignal{
        public RequestCardsDraggingStateSwitchSignal(bool isEnabled){
            this.isEnabled = isEnabled;
        }
        public bool isEnabled;
    }
}
