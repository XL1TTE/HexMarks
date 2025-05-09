using System;
using CMSystem;
using UnityEngine;

namespace Project.Data.CMS.Tags.Generic{

    [Serializable]
    public class TagPrefab : EntityComponentDefinition
    {
        [SerializeField] public GameObject prefab;
    }

}
