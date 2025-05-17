using System;
using System.Collections.Generic;
using CMSystem;
using Project.Actors.Stats;
using UnityEngine;

namespace Project.Data.CMS.Tags.Heroes{
    
    [Serializable]
    public class TagStats: EntityComponentDefinition{
        [SerializeField] private HeroStats m_DefaultStats;
        public HeroStats GetDefaultStats() => m_DefaultStats;
        
        [SerializeField] private List<CMSEntityPfb> m_DefaultDeck;
        public IReadOnlyList<CMSEntityPfb> GetDefaultDeck() => m_DefaultDeck;
    }
    
}
