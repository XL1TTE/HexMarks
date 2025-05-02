using Project.Enemies;
using UnityEngine;

namespace Project.Factories{
    public interface IEnemyViewFactory{
        Enemy CreateEnemy(Enemy prefab, Transform parent);
    }
}

