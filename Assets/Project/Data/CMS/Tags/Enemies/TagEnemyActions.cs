using System;
using CMSystem;
using Project.Enemies.AI;
using UnityEngine;

namespace Project.Data.CMS.Tags.Enemies{
    
    [Serializable]
    public class TagOnTurnActions : EntityComponentDefinition
    {
        [SerializeReference, SubclassSelector] public BaseEnemyAction[] actions;
    }

    [Serializable]
    public class TagOnDieActions: EntityComponentDefinition{
        [SerializeReference, SubclassSelector] public BaseEnemyAction[] actions;
    }
}
