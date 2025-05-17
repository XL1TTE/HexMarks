using System;
using CMSystem;
using Project.Cards;
using UnityEngine;

namespace CardTags{
    
    
    [Serializable]
    public class TagPrefab: EntityComponentDefinition{
        [SerializeField] public CardView prefab;
    }
    
}
