using UnityEngine;
using Zenject;

namespace Project.Factories{
    public class EnemyViewFactory : IEnemyViewFactory
    {
        private readonly DiContainer m_container;

        public EnemyViewFactory(DiContainer container)
        {
            m_container = container;
        }
        public GameObject CreateEnemy(GameObject prefab, Transform parent)
        {
            var instance = m_container.InstantiatePrefab(prefab, parent);
            
            return instance;
        }
    }
}

