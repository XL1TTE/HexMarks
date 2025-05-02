using System.Collections.Generic;
using Project.UI;
using UnityEngine;

namespace Project.Factories{
    public class HealthBarFactory : MonoBehaviour, IHealthBarFactory
    {
        [SerializeField] private Transform m_PoolContainer;        
        [SerializeField] private HealthBar m_HealthBarPrefab;
        private Queue<HealthBar> m_Pool = new();
        
        public HealthBar CreateHealthBar(Transform parent)
        {
            HealthBar healthBar;
            if(m_Pool.Count != 0){
                healthBar = m_Pool.Dequeue();
                healthBar.transform.SetParent(parent);
                healthBar.gameObject.SetActive(true);
            }
            else{
                healthBar = Instantiate(m_HealthBarPrefab, parent);
            }

            var height = GetObjectHeight(parent);

            healthBar.transform.localPosition = Vector3.up * (0.5f + height * 0.5f);

            return healthBar;
        }

        public void ReturnToPool(HealthBar bar)
        {
            bar.gameObject.SetActive(false);
            bar.transform.SetParent(m_PoolContainer);
            
            m_Pool.Enqueue(bar);
        }
        
        
        private float GetObjectHeight(Transform parent){
            var obj = parent.gameObject;
            
            Renderer renderer;
            if(!obj.TryGetComponent<Renderer>(out renderer)){
                renderer = obj.GetComponentInChildren<Renderer>();
            }
            
            float height = 0f;

            if (renderer != null)
            {
                height = renderer.bounds.size.y;
            }
            else
            {
                Collider2D collider;
                if(!obj.TryGetComponent<Collider2D>(out collider)){
                    collider = obj.GetComponentInChildren<Collider2D>();
                }
                if (collider != null)
                {
                    height = collider.bounds.size.y;
                }
            }
            
            return height;
        }
    }
}

