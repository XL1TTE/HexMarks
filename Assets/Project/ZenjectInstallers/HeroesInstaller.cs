using Project.Factories;
using Zenject;

namespace Project.ZenjectInstallers{
    public class HeroesInstaller: MonoInstaller{
        public override void InstallBindings()
        {
            Container.Bind<IHeroViewFactory>().To<HeroViewFactory>().AsSingle();
        }
    }
}

