using System;
using System.Collections;
using DG.Tweening.Plugins;
using UnityEngine;

namespace Enemies.AI{
    
    
    public class EnemyAI{
        public EnemyAI(AI_TargetSelector targetSelector){
            m_targetSelector = targetSelector;
        }
        [SerializeField] AI_TargetSelector m_targetSelector;
    }
    
}
