using Project.Data.SaveFile;
using Project.Factories;
using UnityEngine;
using Zenject;

namespace Project.ZenjectInstallers{
    public class SaveFileInstaller: MonoInstaller{
        
        [SerializeField] InitialSaveConfig m_InitialSaveConfig;        
        public override void InstallBindings()
        {
            Container.Bind<ISaveSystem>().To<SaveSystem>().FromNew().AsSingle()
                .WithArguments(Container, m_InitialSaveConfig);
            
            Container.Bind<SaveFile>()
                .FromNew().AsSingle();
                
            Container.Bind<RuntimeDataProvider>().FromNew().AsSingle();
        }
    }
}

