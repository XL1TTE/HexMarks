using Project.InteractorSystem;
using Zenject;

namespace Project.ZenjectInstallers{
    public class InteractorInstaller: MonoInstaller{

        public override void InstallBindings()
        {
            Container.Bind<Interactor>().AsSingle().NonLazy();
        }
    }
}

