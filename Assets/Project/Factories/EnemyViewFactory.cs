using Project.Enemies;
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
        public Enemy CreateEnemy(Enemy prefab, Transform parent)
        {
            var instance = m_container.InstantiatePrefabForComponent<Enemy>(prefab, parent);
            
            return instance;
        }
    }
}

