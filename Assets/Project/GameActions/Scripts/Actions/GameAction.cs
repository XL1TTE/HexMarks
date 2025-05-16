using System;
using System.Collections;
using Project.Enemies;

namespace XL1TTE.GameActions
{
    
    /* ############################################ */
    /*                   By XL1TTE                  */
    /* ############################################ */
    
    [Serializable]
    public abstract class GameAction
    {
        public abstract void Execute();
    }
    
    [Serializable]
    public abstract class EnemyAction{
        public abstract IEnumerator Execute(EnemyView enemy);
    }
}
