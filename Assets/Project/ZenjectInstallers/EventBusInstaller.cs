
using Zenject;
using Project.EventBus;

namespace Project.ZenjectInstallers{
    public class EventBustInstaller: MonoInstaller{

        public override void InstallBindings()
        {
            Container.Bind<SignalBus>().FromNew().AsSingle();
        }
    }
}
