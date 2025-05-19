using CMSystem;
using Project.Actors;
using Project.Data.CMS.Tags.Generic;
using Project.EventBus;
using UnityEngine;
using Zenject;

namespace Project.Factories{
    public class HeroFactory : IHeroFactory
    {
        private readonly DiContainer m_container;

        public HeroFactory(DiContainer container)
        {
            m_container = container;
        }
        public Hero CreateFromHeroState(HeroState state, Transform parent)
        {
            var model = state.m_model;
            
            var view = m_container.InstantiatePrefabForComponent<HeroView>(model.GetTag<TagPrefab>().prefab, parent);
        
            var hero = new Hero(view, state, m_container.Resolve<SignalBus>());
                             
            return hero;
        }
    }
}

