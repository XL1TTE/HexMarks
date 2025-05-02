
using System.Collections;
using Project.Enemies;
using UnityEngine;

namespace Project.JobSystem{
    public class JobApplyCardEffects : Job
    {
        private Enemy m_Target;
        private float m_Damage;
        public JobApplyCardEffects(Enemy target, float damage){
            m_Target = target;
            m_Damage = damage;
        }
        public override IEnumerator Proccess()
        {
            m_Target.TakeDamage(m_Damage);
            yield break;
        }
    }
}
