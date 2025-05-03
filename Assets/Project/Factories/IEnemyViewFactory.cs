using Project.Enemies;
using UnityEngine;

namespace Project.Factories{
    public interface IEnemyViewFactory{
        EnemyView CreateFromDef(EnemyDefenition def, Transform parent);
    }
}

