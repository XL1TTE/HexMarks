
using Zenject;
using Project.EventBus;
using UnityEngine;

namespace Project.ZenjectInstallers{
    public class EventBustInstaller: MonoInstaller{
        
        [SerializeField] private SignalBus SignalBus;
        
        public override void InstallBindings()
        {
            Container.Bind<SignalBus>().FromInstance(SignalBus).AsSingle();
        }
    }
}
