using Project.Actors.Stats;
using UnityEngine;

namespace Project.Enemies{
    [CreateAssetMenu(fileName = "EnemyDef", menuName = "Enemies/Defenition")]
    public class EnemyDefenition: ScriptableObject{
        [SerializeField] private EnemyView m_Prefab;
        public EnemyView GetPrefab() => m_Prefab;
        [SerializeField] EnemyStats m_Stats;

        [SerializeField] private BaseEnemyDieAnimation m_DieAnimation;

        public EnemyModel GetModel(){
            var model = new EnemyModel(m_Stats);
            model.SetDieAnimation(m_DieAnimation);
            return model;
        }
    }
    
}
