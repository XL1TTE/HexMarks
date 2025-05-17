using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Animations{
    [Serializable]
    public class AnimationWithSFXFrameSettings{
        [SerializeField] public int m_FrameIndex;
        [SerializeField] public List<AudioClip> SFX;
    }
}
