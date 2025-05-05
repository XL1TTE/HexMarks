using Project.Actors.Stats;
using Project.Factories;
using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.ZenjectInstallers{
    public class PlayerDataInstaller: MonoInstaller{
        
        [SerializeReference, SubclassSelector] IPlayerDataFactory m_PlayerDataFactory;        
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>()
                .FromInstance(m_PlayerDataFactory.LoadPlayerData()).AsSingle();
        }
    }
}

