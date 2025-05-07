using System;
using System.Collections;
using UnityEngine;

namespace Project.Enemies{
    
    public abstract class BaseEnemyAnimation
    {
        public abstract IEnumerator GetAnimationRoutine(EnemyView enemyView);
    }
}
