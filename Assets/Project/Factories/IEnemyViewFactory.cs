using UnityEngine;

namespace Project.Factories{
    public interface IEnemyViewFactory{
        GameObject CreateEnemy(GameObject prefab, Transform parent);
    }
}

