using System;
using System.Collections.Generic;
using UnityEngine;
using XL1TTE.GameActions;

namespace XL1TTE.GameAbilities{
    [Serializable]
    internal class EnemyAbilityFrameDefenition{
        public int m_FrameTriggerIndex;
        public List<AudioClip> m_SFX;
        [SerializeReference, SubclassSelector] public List<EnemyAction> m_Actions; 
    }
}
