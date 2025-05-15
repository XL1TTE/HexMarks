using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XL1TTE.Animator{
    
    internal class AwaitableRoutine{
        private Coroutine Coroutine { get; }
        private MonoBehaviour Owner { get; }
        public bool IsDone { get; private set; } = false;

        public AwaitableRoutine(MonoBehaviour owner, IEnumerator routine)
        {
            Coroutine = owner.StartCoroutine(Run(routine));
            Owner = owner;
        }

        private IEnumerator Run(IEnumerator routine)
        {
            yield return routine;
            IsDone = true;
        }

        public void Kill()
        {
            Owner.StopCoroutine(Coroutine);
        }
    }
        
    public abstract class xlAnimation{
        private static AnimatorComponent m_Animator;

        protected xlAnimation(){
            if(m_Animator == null){
                m_Animator = new GameObject("[Animator]", new Type[1]{typeof(AnimatorComponent)}).GetComponent<AnimatorComponent>();
                UnityEngine.Object.DontDestroyOnLoad(m_Animator); 
            }
        }

        protected const int MAX_LOOPS = 10;

        public bool isCompleted {
            get => m_Animator.isAnimationCompleted(this);
            protected set {}
        }
        
        protected int m_loops = 1;
        
        
        public xlAnimation Play(){
            m_Animator.OnPlayAnimation(this);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loops">-1 if infinite.</param>
        public xlAnimation SetLoops(int loops = -1){
            m_loops = loops;
            return this;
        }
        
        public void Stop(){}
        public void Kill(){
            m_Animator.OnKillAnimation(this);   
        }
        
        internal abstract IEnumerator GetAnimation();
    }
    
    
    public delegate void FrameCallback();
    public class FrameAnimation : xlAnimation{
        public class Frame{
            public Frame(Sprite sprite, float duration = 100){this.sprite = sprite; m_duration = duration;} 
            ~Frame() {callback = null;}
            
            private Sprite sprite;
            private float m_duration;
            private FrameCallback callback;

            internal Sprite GetSprite() => sprite; 
            internal float GetDuration() => m_duration;           
            public void AddCallback(FrameCallback callback) => this.callback += callback;
            internal void OnEndofFrame(){callback?.Invoke();}   
        }
        
        public FrameAnimation(SpriteRenderer renderer, IEnumerable<Frame> frames) : base(){
            m_frames = frames.ToArray();
            m_renderer = renderer;
        }
        private readonly SpriteRenderer m_renderer;
        private readonly Frame[] m_frames;
        
        public void AddFrameCallback(int frameIndex, FrameCallback callback)
        {
            if(frameIndex < 0 || frameIndex >= m_frames.Length){
                throw new IndexOutOfRangeException("Frame index was out of range.");
            }
            
            m_frames[frameIndex].AddCallback(callback);
        }
        
        internal override IEnumerator GetAnimation()
        {
            int loopsCompleted = 0;
            while (m_loops == -1 || loopsCompleted < m_loops)
            {
                foreach (var frame in m_frames)
                {
                    m_renderer.sprite = frame.GetSprite();
                    yield return new WaitForSeconds(frame.GetDuration() / 1000f);
                    frame.OnEndofFrame();
                }

                loopsCompleted++;
            }
        }
    }
    
    public class WaitForCompletion: CustomYieldInstruction{
        
        public WaitForCompletion(xlAnimation animation){
            m_animation = animation;
        }
        
        private xlAnimation m_animation;

        public override bool keepWaiting => !m_animation.isCompleted;
    }
    
}
