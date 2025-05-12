using System.Collections;
using Project.GameManagers;
using UnityEngine;

namespace Project.Game.Battle{
    
    
    public class BattleMain: MonoBehaviour{

        [SerializeField] BattleManager m_battleManager;

        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            
            m_battleManager.StartBattle();
        }

    }
}
