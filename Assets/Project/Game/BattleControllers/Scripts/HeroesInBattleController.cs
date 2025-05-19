using System.Collections.Generic;
using Project.Actors;
using Project.EventBus;
using Project.EventBus.Signals;
using UnityEngine;
using Zenject;

namespace Project.Game.Battle.Controllers
{
    public class HeroesInBattleController: MonoBehaviour{

        [Inject]
        private void Construct(SignalBus signalBus){
            m_SignalBus = signalBus;
        }
    
        void OnEnable()
        {
            m_SignalBus.Subscribe<HeroDamageTakenSignal>(OnHeroDamageTaken);
        }

        void OnDisable()
        {
            m_SignalBus.Unsubscribe<HeroDamageTakenSignal>(OnHeroDamageTaken);

        }

        private SignalBus m_SignalBus;
        
        private void OnHeroDamageTaken(HeroDamageTakenSignal signal)
        {
            var hero = signal.hero;
            
            if (isHeroJustDied(hero))
            {
                NotifyHeroDied(hero);
            }
        }

        private void NotifyHeroDied(Hero hero) =>
            m_SignalBus.SendSignal(new HeroDiedSignal(hero));
        
        private bool isHeroJustDied(Hero hero) =>
            hero.GetCurrentHealth() == 0;
    }
}
