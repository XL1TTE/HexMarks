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
            Container.Bind<CardViewObjectPool>().FromMethod((c) => ConfigureCardPool(c)).AsSingle();
            
            Container.Bind<ICardFactory>()
                .To<CardFactory>()
                .AsSingle();        
        }
        
        
        private CardViewObjectPool ConfigureCardPool(InjectContext context){
            return m_CardViewPool.Init(context.Container);
        }
    }
}

