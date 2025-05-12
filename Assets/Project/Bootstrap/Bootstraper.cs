
using CMSystem;
using Project.Data.SaveFile;
using Project.Factories;
using Project.Utilities;
using UnityEngine;
using Zenject;

namespace Project.Bootstrap{
    public class Bootstraper : MonoBehaviour{

        [SerializeField] ToolTipManager g_ToolTipManager;
        
        [Inject]
        private void Construct(ISaveSystem saveSystem){
            m_saveSystem = saveSystem;
        }
        private ISaveSystem m_saveSystem;

        void Awake()
        {
            g_ToolTipManager.Initialize();
        }

    }
}
