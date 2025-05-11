using CMSystem;
using Project.Actors;
using UnityEngine;

namespace Project.Factories{
    public interface IHeroViewFactory{
        HeroView CreateFromSaveHeroState(SaveHeroState state, Transform parent);
    }
}

