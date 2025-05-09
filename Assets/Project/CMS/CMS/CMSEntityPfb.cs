using System.Collections.Generic;
using UnityEngine;

namespace CMSystem{
    public class CMSEntityPfb : MonoBehaviour
    {
        [SerializeReference, SubclassSelector]
        public List<EntityComponentDefinition> Components;

        public string GetId()
        {
            #if UNITY_EDITOR
            string path = UnityEditor.AssetDatabase.GetAssetPath(gameObject);

            if (path.StartsWith("Assets/Resources/") && path.EndsWith(".prefab"))
            {
                path = path.Substring("Assets/Resources/".Length);
                path = path.Substring(0, path.Length - ".prefab".Length);
            }

            return path;
            #endif
        }
    }
}
