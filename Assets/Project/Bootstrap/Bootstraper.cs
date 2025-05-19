using System.Collections.Generic;
using Project.DataResolving.DataRequestResolvers;
using Project.LoggingSystem;
using Project.Utilities;
using UnityEngine;
using XL1TTE.GameActions;
using Zenject;

namespace Project.Bootstrap{
    public class Bootstraper : MonoBehaviour{

        [SerializeField] ToolTipManager g_ToolTipManager;

        void Awake()
        {     
            FloatingIconUtility.Init();
            
            g_ToolTipManager.Initialize();
        }

    }
}
