using System.Collections.Generic;
using Project.Cards;
using Project.Factories;
using UnityEngine;
using Zenject;

namespace Project.ZenjectInstallers{
    public class CardInstaller : MonoInstaller
    {
        [SerializeField] private CardViewObjectPool m_CardViewPool;        
        public override void InstallBindings()
        {
            Container.Bind<CardViewObjectPool>().FromInstance(m_CardViewPool).AsSingle();
            
            Container.Bind<ICardFactory>()
                .To<CardFactory>()
                .AsSingle();        
        }
    }
}

