namespace Project.EventBus.Signals{
    
    public class BattleStateChangedSignal: ISignal{
        
        public string m_StateChangedMessage;
        public BattleStateChangedSignal(string a_message){
            m_StateChangedMessage = a_message;
        }
    }
    
}
