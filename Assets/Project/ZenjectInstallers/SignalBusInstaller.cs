
using Zenject;
using Project.EventBus;
using UnityEngine;

namespace Project.ZenjectInstallers{
    public class SignalBustInstaller: MonoInstaller{
        
        public override void InstallBindings()
        {
            Container.Bind<SignalBus>().FromNew().AsSingle();
        }
    }
}
