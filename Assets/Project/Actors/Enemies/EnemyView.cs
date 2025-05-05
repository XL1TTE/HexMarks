using System.Collections;
using DG.Tweening;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.JobSystem;
using UnityEngine;
using Zenject;

namespace Project.Enemies
{
    public class EnemyView : MonoBehaviour{

        public void Init(Enemy controller){
            m_controller = controller;
        }
        private Enemy m_controller;
        public Enemy GetController() => m_controller;
        
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        public SpriteRenderer GetRenderer() => m_spriteRenderer;
    }
}
