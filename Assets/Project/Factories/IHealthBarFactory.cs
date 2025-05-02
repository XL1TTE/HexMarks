using Project.UI;
using UnityEngine;

namespace Project.Factories{
    public interface IHealthBarFactory{
        HealthBar CreateHealthBar(Transform parent);
        void ReturnToPool(HealthBar bar);
    }
}

