using System;
using Project.Actors;
using UnityEngine;

namespace Enemies.AI{

    [Serializable]
    public class RuleFactor
    {
        [SerializeField] public float m_value;
        [SerializeField] public float m_weightPerValue;
    }

    [Serializable]
    public abstract class BaseSelectTargetRule{
        public abstract float CalculateWeight(ITargetable target);
    }
}
