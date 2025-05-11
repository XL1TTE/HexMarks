using UnityEngine;

namespace Project.Utilities{
    
    public class ConstantObject: MonoBehaviour{
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
}
