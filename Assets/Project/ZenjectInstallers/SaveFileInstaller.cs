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
                .WithArguments(m_InitialSaveConfig);

            Container.Bind<SaveFile>().FromMethod(LoadSaveFile).AsSingle().NonLazy();

            Container.Bind<RuntimeDataProvider>().FromNew().AsSingle();
        }


        private SaveFile LoadSaveFile(InjectContext context)
        {
            var saveSystem = context.Container.Resolve<ISaveSystem>();

            return saveSystem.LoadSave();
        }
    }
}

