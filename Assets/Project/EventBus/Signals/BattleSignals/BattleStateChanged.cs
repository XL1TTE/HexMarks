

using UnityEngine;

namespace Project.EventBus.Signals{
    
    public class BattleStateChanged: ISignal{
        
        public string m_StateChangedMessage;
        public BattleStateChanged(string a_message){
            m_StateChangedMessage = a_message;
        }
    }
    
    
    public class SetEnemyTargetSignal : ISignal{
        
        private GameObject m_target;
        public GameObject GetTarget() => m_target;
        public SetEnemyTargetSignal(GameObject target){
            m_target = target;
        }
    }
}
