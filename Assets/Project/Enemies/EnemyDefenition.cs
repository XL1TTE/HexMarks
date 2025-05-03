using UnityEngine;

namespace Project.Enemies{
    [CreateAssetMenu(fileName = "EnemyDef", menuName = "Enemies/Defenition")]
    public class EnemyDefenition: ScriptableObject{
        [SerializeField] private EnemyView m_Prefab;
        public EnemyView GetPrefab() => m_Prefab;
        [SerializeField, Range(1f, 10000f)] private float m_Health = 10f;
        [SerializeField, Range(1f, 10000f)] private float m_MaxHealth = 10f;
        [SerializeField, Range(1f, 10000f)] private float m_Damage = 1f;
        [SerializeField, Range(1f, 100f)] private float m_Initiative = 1f;

        [SerializeField] private BaseEnemyDieAnimation m_DieAnimation;

        public EnemyModel GetModel(){
            var model = new EnemyModel(m_Health, m_MaxHealth, m_Damage, m_Initiative);
            model.SetDieAnimation(m_DieAnimation);
            return model;
        }
    }
    
}
