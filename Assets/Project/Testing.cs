using System;
using CMSystem;
using Project.EventBus;
using Project.Game.Battle;
using Project.GameManagers;
using Project.Player;
using UnityEngine;
using Zenject;

namespace Project
{
    public class Testing : MonoBehaviour
    {

        void Start()
        {
            
            CMS.Init();
                        
            battleController.Initialize();
            
            battleController.StartBattle();
        }

        
        [SerializeField] BattleController battleController;
        [SerializeField] BattleSequenceManager battleManager;
    }
}

