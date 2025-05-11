using UnityEngine;

namespace Project.Actors{
    public class HeroView: MonoBehaviour{
        public void Init(HeroState heroState)
        {
            m_State = heroState;
        }
        private HeroState m_State;
        
        public HeroState GetState() => m_State;
        
        public int GetInitiative() => m_State.GetInitiative();

    }
    
}
