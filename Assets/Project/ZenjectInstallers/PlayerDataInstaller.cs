using Project.Factories;
using Project.Player;
using Zenject;

namespace Project.ZenjectInstallers{
    public class PlayerDataInstaller: MonoInstaller{

        public override void InstallBindings()
        {
            Container.Bind<PlayerData>().FromNew().AsSingle();
            Container.Bind<IPlayerDataSaver>().To<SaveSystem>().FromNew().AsSingle();
        }
    }
}

