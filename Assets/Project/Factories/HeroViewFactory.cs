using CMSystem;
using Project.Actors;
using Project.Data.CMS.Tags.Generic;
using Project.Data.CMS.Tags.Heroes;
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
        public HeroView CreateFromSaveHeroState(SaveHeroState state, Transform parent)
        {
            var model = CMS.Get<CMSEntity>(state.m_ModelID);
            
            var view = m_container.InstantiatePrefabForComponent<HeroView>(model.GetTag<TagPrefab>().prefab, parent);
             
            var heroState = new HeroState(state.id, state.m_Stats, model);

            view.Init(heroState);
            
            return view;
        }
    }
}

