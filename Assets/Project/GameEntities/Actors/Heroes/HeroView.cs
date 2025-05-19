using System;
using UnityEngine;

namespace Project.Actors{
    public class HeroView: MonoBehaviour{
        
        public event Action OnSelectedAsCardTarget;
        
        public void SelectAsCardTarget(){
            OnSelectedAsCardTarget?.Invoke();
        }      
    }
    
}
