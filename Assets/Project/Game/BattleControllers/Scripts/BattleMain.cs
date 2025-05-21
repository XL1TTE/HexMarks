using System.Collections;
using Project.GameManagers;
using Project.InteractorSystem;
using UnityEngine;
using Zenject;

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
