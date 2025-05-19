using System;
using System.Collections.Generic;
using System.Linq;
using Project.Actors;
using UnityEngine;

namespace Enemies.AI{
    [Serializable]
    public class AI_TargetSelector{
        private class TargetScore{public ITargetable m_target; public float m_score;}
        
        [SerializeReference, SubclassSelector] private BaseSelectTargetRule[] m_rules;
        public ITargetable SelectTarget(IEnumerable<ITargetable> targets){
            TargetScore[] _map = new TargetScore[targets.Count()];
            
            foreach(var target in targets){
                float total_weight = 0; 
                foreach(var rule in m_rules){
                    total_weight += rule.CalculateWeight(target);
                }
                _map.Append(new TargetScore{m_target=target, m_score=total_weight});
            }
            
            TargetScore current_best = _map.First();
            foreach(var record in _map){
                if(current_best.m_score < record.m_score){current_best = record;}
            }
            
            return current_best.m_target;
        }
    }
}
