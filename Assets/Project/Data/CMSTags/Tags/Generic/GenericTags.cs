using System;
using CMSystem;
using UnityEngine;

namespace Project.Data.CMS.Tags.Generic{

    [Serializable]
    public class TagPrefab : EntityComponentDefinition
    {
        [SerializeField] public GameObject prefab;
    }

    [Serializable]
    public class TagName : EntityComponentDefinition{
        [SerializeField] public string name;
    }
    
    [Serializable]
    public class TagDescription: EntityComponentDefinition{
        [SerializeField, TextArea] public string desc;
    }

}
