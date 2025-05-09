using System.Collections;
using DG.Tweening;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.JobSystem;
using Unity.VisualScripting;
using UnityEngine;
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


        private Coroutine m_IdleCoroutine;
        

        public void StartIdleAnimation(){
            
            if(m_IdleCoroutine != null){return;}
            m_IdleCoroutine = StartCoroutine(IdleAnimation());
        }
        public void StopIdleAnimation(){
            if(m_IdleCoroutine != null){
                StopCoroutine(m_IdleCoroutine);
                m_IdleCoroutine = null;
            }

        }
        
        private IEnumerator IdleAnimation(){
            while(true){
               yield return m_controller.GetIdleAnimation();
            }
        }
    }
}
