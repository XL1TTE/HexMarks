
using System.Collections;
using Project.Enemies;
using UnityEngine;

namespace Project.JobSystem{
    public class JobApplyCardEffects : Job
    {
        private EnemyView m_Target;
        private float m_Damage;
        public JobApplyCardEffects(EnemyView target, float damage){
            m_Target = target;
            m_Damage = damage;
        }
        public override IEnumerator Proccess()
        {
            m_Target.GetController().TakeDamage(m_Damage);
            yield break;
        }
    }
}
