using System;
using CMSystem;
using Enemies.AI;
using Enemies.Animations;
using Project.Actors.Stats;
using Project.Enemies;
using Project.Enemies.Abilities;
using UnityEngine;
using XL1TTE.GameAbilities;

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
    
    
    [Serializable]
    public class TagEnemyAbilities : EntityComponentDefinition{    
        [SerializeReference, SubclassSelector] public EnemyAblilityInspector m_basicAttackAbility; 
        [SerializeReference, SubclassSelector] public EnemyAblilityInspector m_superAttackAbility; 
    }
    
    
    [Serializable]
    public class TagAI: EntityComponentDefinition{
        [SerializeField] public AI_TargetSelector m_targetSelector;
        
        public EnemyAI GetAI() {
            return new EnemyAI(m_targetSelector);
        }
    }
}
