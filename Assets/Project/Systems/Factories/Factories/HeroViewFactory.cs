using CMSystem;
using Project.Actors;
using Project.Data.CMS.Tags.Generic;
using UnityEngine;
using Zenject;

namespace Project.Factories{
    public class HeroViewFactory : IHeroViewFactory
    {
        private readonly DiContainer m_container;

        public HeroViewFactory(DiContainer container)
        {
            m_container = container;
        }
        public HeroView CreateFromHeroState(HeroState state, Transform parent)
        {
            var model = state.m_model;
            
            var view = m_container.InstantiatePrefabForComponent<HeroView>(model.GetTag<TagPrefab>().prefab, parent);
             
            view.Init(state);
            
            return view;
        }
    }
}

