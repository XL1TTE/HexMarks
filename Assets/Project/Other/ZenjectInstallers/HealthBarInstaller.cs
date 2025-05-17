using Project.Factories;
using UnityEngine;
using Zenject;

namespace Project.ZenjectInstallers{
    public class HealthBarInstaller: MonoInstaller{
        [SerializeField] private HealthBarFactory m_Factory;

        public override void InstallBindings()
        {
            Container.Bind<IHealthBarFactory>().To<HealthBarFactory>().FromInstance(m_Factory).AsSingle();
        }
    }
}

