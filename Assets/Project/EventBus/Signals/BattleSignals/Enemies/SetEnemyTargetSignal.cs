

using Project.Enemies;
using UnityEngine;

namespace Project.EventBus.Signals{
    public class SetEnemyTargetSignal : ISignal{
        
        private EnemyView m_target;
        public EnemyView GetTarget() => m_target;
        public SetEnemyTargetSignal(EnemyView target){
            m_target = target;
        }
    }
    
}
