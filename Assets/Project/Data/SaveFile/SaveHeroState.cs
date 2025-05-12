using System;
using CMSystem;
using Project.Actors.Stats;

namespace Project.Actors{
    
    [Serializable]
    public class SaveHeroState {
        public SaveHeroState(HeroStats stats, string modelID)
        {
            m_Stats = stats;

            m_ModelID = modelID;
            
            id = Guid.NewGuid().ToString();
        }

        public string id;
        public HeroStats m_Stats;
        public string m_ModelID;
    }
    
}
