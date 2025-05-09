using CMSystem;
using Project.Enemies;
using UnityEngine;

namespace Project.Factories{
    public interface IEnemyViewFactory{    
        EnemyView CreateFromCMS(CMSEntityPfb prefab, Transform parent);
    }
}

