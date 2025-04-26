using Project.Cards;
using Project.Factories;
using UnityEngine;
using Zenject;

namespace Project.ZenjectInstallers{
    public class CardInstaller : MonoInstaller
    {
        [SerializeField] CardView m_cardPrefab;

        public override void InstallBindings()
        {
            Container.Bind<ICardFactory>()
                .To<CardFactory>()
                .AsSingle()
                .WithArguments(m_cardPrefab);
        }
    }
}

