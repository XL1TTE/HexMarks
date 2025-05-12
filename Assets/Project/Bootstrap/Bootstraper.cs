
using CMSystem;
using Project.Data.SaveFile;
using Project.Factories;
using Project.Utilities;
using UnityEngine;
using Zenject;

namespace Project.Bootstrap{
    public class Bootstraper : MonoBehaviour{

        [SerializeField] ToolTipManager g_ToolTipManager;

        void Awake()
        {
            g_ToolTipManager.Initialize();
        }

    }
}
