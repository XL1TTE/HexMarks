using System;
using System.Collections;
using UnityEngine;

namespace Project.Enemies{
    
    public abstract class BaseEnemyDieAnimation : ScriptableObject
    {
        public abstract IEnumerator GetDieSequence(EnemyView enemyView);
    }
}
