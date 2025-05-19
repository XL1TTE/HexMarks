using System.Collections.Generic;
using Project.DataResolving;
using Project.DataResolving.DataRequestResolvers;
using UnityEngine;
using XL1TTE.GameActions;
using Zenject;

namespace Project.ZenjectInstallers{
    public class DataResoverInstaller: MonoInstaller{
        public override void InstallBindings()
        {            
            /* ########################################## */
            /*           Data Request Resolvers           */
            /* ########################################## */
            
            Container.Bind<EnemyTargetResolver>().FromNew().AsSingle();
            
            Container.Bind<HeroesInBattleReqResolver>().FromNew().AsSingle();
            
            Container.Bind<EnemiesInBattleReqResolver>().FromNew().AsSingle();
            
            Container.Bind<CardHandReqResolver>().FromNew().AsSingle();    
            
            Container.Bind<LastAllyCardPlayedReqResolver>().FromNew().AsSingle();    
            
            Container.Bind<ContextResolver>().FromMethod(
                (c) => InitializeContextResolver(c)).AsSingle();
        }

        ContextResolver InitializeContextResolver(InjectContext context)
        {
            var res1 = context.Container.Resolve<EnemyTargetResolver>();
            var res2 = context.Container.Resolve<HeroesInBattleReqResolver>();
            var res3 = context.Container.Resolve<EnemiesInBattleReqResolver>();
            var res4 = context.Container.Resolve<CardHandReqResolver>();
            var res5 = context.Container.Resolve<LastAllyCardPlayedReqResolver>();
            
            var contextResolver = new ContextResolver(new List<IDataRequestResolver>{
                res1, res2, res3, res4, res5
            });

            Debug.Log("LOAD CONTEXT RESOLVER");
            
            return contextResolver;
        }
    }
}

