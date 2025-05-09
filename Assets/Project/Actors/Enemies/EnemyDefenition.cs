using Project.Actors.Stats;
using UnityEngine;

namespace Project.Enemies{
    [CreateAssetMenu(fileName = "EnemyDef", menuName = "Enemies/Defenition")]
    public class EnemyDefenition: ScriptableObject{
        [SerializeField] private EnemyView m_Prefab;
        public EnemyView GetPrefab() => m_Prefab;
        [SerializeField] EnemyStats m_Stats;

        [SerializeReference, SubclassSelector] private BaseEnemyAnimation m_DieAnimation;
        [SerializeReference, SubclassSelector] private BaseEnemyAnimation m_IdleAnimation;
        [SerializeReference, SubclassSelector] private BaseEnemyAnimation m_AttackAnimation;

        // public EnemyState GetModel(){
        //     var model = new EnemyModel(m_Stats);
        //     model.SetDieAnimation(m_DieAnimation);
        //     model.SetAttackAnimation(m_AttackAnimation);
        //     model.SetIdleAnimation(m_IdleAnimation);

        //     return model;
        // }
    }
    
}
