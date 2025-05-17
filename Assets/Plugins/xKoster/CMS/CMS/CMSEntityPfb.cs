using System;
using System.Collections.Generic;
using UnityEngine;

namespace CMSystem{
    public class CMSEntityPfb : MonoBehaviour
    {
        
        [SerializeReference, SubclassSelector] public List<EntityComponentDefinition> Components;

        public string GetId(){
            return gameObject.name;
        }
    }
}
