using System;
using Project.Actors;
using UnityEngine;

namespace Enemies.AI{
    [Serializable]
    public class TargetHealthRule : BaseSelectTargetRule
    {
        [SerializeField] private RuleFactor m_factor;

        public override float CalculateWeight(ITargetable target) => 
            (target.GetCurrentHealth() / m_factor.m_value) * m_factor.m_weightPerValue;
    }
}
