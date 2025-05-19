using Project.Factories;
using Zenject;

namespace Project.ZenjectInstallers{
    public class HeroesInstaller: MonoInstaller{
        public override void InstallBindings()
        {
            Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
        }
    }
}

