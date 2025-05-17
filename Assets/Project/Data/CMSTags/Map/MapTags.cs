
using System;
using System.Collections.Generic;
using CMSystem;
using Project.Map;
using UnityEngine;

namespace Project.Data.CMS.Tags{
    
    [Serializable]
    public class TagMapLocation : EntityComponentDefinition{}

    [Serializable]
    public class TagDungeon: TagMapLocation{
        [SerializeField] private List<CMSEntityPfb> m_Enemies = new();
        public IReadOnlyList<CMSEntityPfb> GetEnemies() => m_Enemies;
    }
    
}
