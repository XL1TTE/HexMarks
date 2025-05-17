
using System.Collections;
using Project.Sound;
using UnityEngine;

namespace Project.JobSystem{
    public class JobPlaySound : Job
    {
        public JobPlaySound(AudioClip clip, SoundChannel channel)
        {
            m_clip = clip;
            m_channel = channel;
        }
        private AudioClip m_clip;
        private SoundChannel m_channel;

        public override IEnumerator Proccess()
        {
            m_channel.PlaySound(m_clip);
            yield break;
        }
    }
}

