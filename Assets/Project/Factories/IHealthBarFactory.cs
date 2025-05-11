using Project.UI;
using UnityEngine;

namespace Project.Factories{
    public interface IHealthBarFactory{
        
        enum BarAlignment {
            UP,
            BUTTOM    
        }
        
        HealthBar CreateEnemyHealthBar(Transform parent);
        public HealthBar CreateHeroHealthBar(Transform parent, BarAlignment alignment);
        void ReturnEnemyBarToPool(HealthBar bar);
        void ReturnHeroBarToPool(HealthBar bar);
    }
}

