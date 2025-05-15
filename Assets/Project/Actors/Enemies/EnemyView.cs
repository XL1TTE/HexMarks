using System.Collections;
using DG.Tweening;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.JobSystem;
using Unity.VisualScripting;
using UnityEngine;
using XL1TTE.Animator;
using Zenject;

namespace Project.Enemies
{
    public class EnemyView : MonoBehaviour{

        public void Init(Enemy controller){
            m_controller = controller;
        }

        void OnDestroy()
        {
            StopIdleAnimation();
        }

        private Enemy m_controller;
        public Enemy GetController() => m_controller;
        
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        public SpriteRenderer GetRenderer() => m_spriteRenderer;


        private xlAnimation m_IdleAnimation;
        

        public void StartIdleAnimation(){
            
            if(m_IdleAnimation != null){return;}
            m_IdleAnimation = m_controller.GetIdleAnimation().SetLoops(-1).Play();
        }
        public void StopIdleAnimation(){
            if(m_IdleAnimation != null){
                m_IdleAnimation.Kill();
                m_IdleAnimation = null;
            }

        }
    }
}
