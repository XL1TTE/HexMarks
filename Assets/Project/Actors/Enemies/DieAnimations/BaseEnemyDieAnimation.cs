using System;
using System.Collections;
using UnityEngine;

namespace Project.Enemies{
    
    public abstract class BaseEnemyAnimation
    {
        public abstract XL1TTE.Animator.xlAnimation GetAnimation(EnemyView enemyView);
    }
}
