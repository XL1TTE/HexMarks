using CMSystem;
using Project.Actors;
using UnityEngine;

namespace Project.Factories{
    public interface IHeroViewFactory{
        HeroView CreateFromHeroState(HeroState state, Transform parent);
    }
}

