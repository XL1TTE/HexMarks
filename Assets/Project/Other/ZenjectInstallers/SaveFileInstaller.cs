using Project.Factories;
using GameData;
using UnityEngine;
using Zenject;

namespace Project.ZenjectInstallers{
    public class SaveFileInstaller: MonoInstaller{
        
        [SerializeField] InitialSaveConfig m_InitialSaveConfig;        
        public override void InstallBindings()
        {
            Container.Bind<ISaveSystem>().To<SaveSystem>().FromNew().AsSingle()
                .WithArguments(m_InitialSaveConfig);
                
            Container.Bind<GameDataTracker>().FromNew().AsSingle();
        }
    }
}

