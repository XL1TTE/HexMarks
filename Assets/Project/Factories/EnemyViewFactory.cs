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
        
        public EnemyView CreateFromDef(EnemyDefenition def, Transform parent)
        {
            var view = m_container.InstantiatePrefabForComponent<EnemyView>(def.GetPrefab(), parent);
            var model = def.GetModel();
            
            var enemyController = new Enemy(view, model);
            
            return view;
        }
    }
}

