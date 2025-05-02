

using Project.Enemies;
using UnityEngine;

namespace Project.EventBus.Signals{
    public class SetEnemyTargetSignal : ISignal{
        
        private Enemy m_target;
        public Enemy GetTarget() => m_target;
        public SetEnemyTargetSignal(Enemy target){
            m_target = target;
        }
    }
    
}
