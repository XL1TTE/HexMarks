using CMSystem;
using Project.Game.Battle;
using UnityEngine;

namespace Project
{
    public class Testing : MonoBehaviour
    {

        void Start()
        {                 
            battleController.Initialize();
            
            battleController.StartBattle();
        }

        
        [SerializeField] BattleController battleController;
    }
}

