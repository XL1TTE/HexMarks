using Project.Factories;
using Zenject;

namespace Project.ZenjectInstallers{
    public class EnemyInstaller: MonoInstaller{
        public override void InstallBindings()
        {
            Container.Bind<IEnemyViewFactory>().To<EnemyViewFactory>().AsSingle();
        }
    }
}

