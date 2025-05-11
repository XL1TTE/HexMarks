using Project.DataResolving;
using Project.DataResolving.DataRequestResolvers;
using Zenject;

namespace Project.ZenjectInstallers{
    public class DataResoverInstaller: MonoInstaller{
        public override void InstallBindings()
        {
            Container.Bind<DataResolver>().FromNew().AsSingle();

            
            //Here we bind all of our Data Request Resolvers//
            
            Container.Bind<EnemyTargetResolver>().FromNew().AsSingle();
            
            Container.Bind<EnemiesInBattleReqResolver>().FromNew().AsSingle();
            
            Container.Bind<CardHandReqResolver>().FromNew().AsSingle();
        }
    }
}

