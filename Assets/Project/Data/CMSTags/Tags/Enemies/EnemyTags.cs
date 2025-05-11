using System;
using CMSystem;
using Project.Actors.Stats;
using Project.Enemies;
using UnityEngine;

namespace Project.Data.CMS.Tags.Enemies{


    public class TagEnemyModel : EntityComponentDefinition {}

    [Serializable]
    public class TagStats : EntityComponentDefinition
    {
        [SerializeField] public EnemyStats m_stats;
    }

    [Serializable]
    public class TagAnimations : EntityComponentDefinition
    {
        [SerializeReference, SubclassSelector] public BaseEnemyAnimation m_DieAnimation;
        [SerializeReference, SubclassSelector] public BaseEnemyAnimation m_IdleAnimation;
        [SerializeReference, SubclassSelector] public BaseEnemyAnimation m_AttackAnimation;
    }
    
    
    

}
