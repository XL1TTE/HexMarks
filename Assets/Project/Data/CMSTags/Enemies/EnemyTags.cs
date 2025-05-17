using System;
using CMSystem;
using Enemies.Animations;
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
        [SerializeField] public AnimationDefenition m_DieAnimation;
        [SerializeField] public AnimationDefenition m_IdleAnimation;
    }
    
    
    

}
