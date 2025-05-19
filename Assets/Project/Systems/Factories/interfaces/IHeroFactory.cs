using CMSystem;
using Project.Actors;
using UnityEngine;

namespace Project.Factories{
    public interface IHeroFactory{
        Hero CreateFromHeroState(HeroState state, Transform parent);
    }
}

