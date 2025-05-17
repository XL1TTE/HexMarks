using CMSystem;
using Project.Actors.Stats;
using Project.Data.CMS.Tags.Enemies;
using Project.Data.CMS.Tags.Generic;
using Project.Enemies;
using UnityEngine;
using Zenject;

namespace Project.Factories{
    public class EnemyViewFactory : IEnemyViewFactory
    {
        private readonly DiContainer m_container;

        public EnemyViewFactory(DiContainer container)
        {
            m_container = container;
        }

        public EnemyView CreateFromCMS(CMSEntityPfb prefab, Transform parent)
        {
            var model = CMS.Get<CMSEntity>(prefab.GetId());
            
            var view = m_container.InstantiatePrefabForComponent<EnemyView>(model.GetTag<TagPrefab>().prefab, parent);
            
            EnemyStats stats = model.GetTag<TagStats>().m_stats;
            TagOnTurnAbilities ai = model.GetTag<TagOnTurnAbilities>();
            
            var e_state = new EnemyState(stats, model);
            
            var t_animaions = model.GetTag<TagAnimations>();
            
            e_state.SetIdleAnimation(t_animaions.m_IdleAnimation);
            e_state.SetDieAnimation(t_animaions.m_DieAnimation);
            
            var enemyController = new Enemy(view, e_state);
            
            return view;
        }
    }
}

