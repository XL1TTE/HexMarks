using System.Collections.Generic;
using Project.UI;
using Project.Utilities.Extantions;
using UnityEngine;

namespace Project.Factories{
    public class HealthBarFactory : MonoBehaviour, IHealthBarFactory
    {
        [SerializeField] private Transform m_PoolContainer;        
        [SerializeField] private HealthBar m_EnemyHealthBarPrefab;
        [SerializeField] private HealthBar m_HeroesHealthBarPrefab;
        private Queue<HealthBar> m_EnemyHealthBarPool = new();
        private Queue<HealthBar> m_HeroHealthBarPool = new();
        
        public HealthBar CreateEnemyHealthBar(Transform parent)
        {
            HealthBar healthBar;
            if (m_EnemyHealthBarPool.Count != 0)
            {
                healthBar = m_EnemyHealthBarPool.Dequeue();
                healthBar.transform.SetParent(parent);
                healthBar.gameObject.SetActive(true);
            }
            else
            {
                healthBar = Instantiate(m_EnemyHealthBarPrefab, parent);
            }

            var height = parent.GetHeight();

            healthBar.transform.localPosition = Vector3.up * (0.5f + height * 0.5f);

            return healthBar;
        }
        public HealthBar CreateHeroHealthBar(Transform parent, IHealthBarFactory.BarAlignment alignment)
        {
            HealthBar healthBar;
            if (m_HeroHealthBarPool.Count != 0)
            {
                healthBar = m_HeroHealthBarPool.Dequeue();
                healthBar.transform.SetParent(parent);
                healthBar.gameObject.SetActive(true);
            }
            else
            {
                healthBar = Instantiate(m_HeroesHealthBarPrefab, parent);
            }

            var height = parent.GetHeight();
            
            if(alignment == IHealthBarFactory.BarAlignment.UP){
                healthBar.transform.localPosition = Vector3.up * (0.5f + height * 0.5f);
            }
            else if(alignment == IHealthBarFactory.BarAlignment.BUTTOM){
                healthBar.transform.localPosition = Vector3.down * (0.5f + height * 0.5f);
            }

            return healthBar;
        }

        public void ReturnEnemyBarToPool(HealthBar bar)
        {
            bar.gameObject.SetActive(false);
            bar.transform.SetParent(m_PoolContainer);
            m_EnemyHealthBarPool.Enqueue(bar);
        }
        public void ReturnHeroBarToPool(HealthBar bar)
        {
            bar.gameObject.SetActive(false);
            bar.transform.SetParent(m_PoolContainer);
            m_HeroHealthBarPool.Enqueue(bar);
        }
    }
}

