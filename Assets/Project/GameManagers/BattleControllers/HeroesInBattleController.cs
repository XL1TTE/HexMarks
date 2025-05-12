using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using ModestTree;
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
            m_SignalBus.Subscribe<BattleStageReadySignal>(BattleStageProccess);
        }

        void OnDisable()
        {
            m_SignalBus.Unsubscribe<BattleStageReadySignal>(BattleStageProccess);
        }

        private SignalBus m_SignalBus;
        
        private IReadOnlyList<HeroView> m_Heroes;      
        
          
        private void BattleStageProccess(BattleStageReadySignal signal)
        {
            FreePreviousBattleStage();

            m_Heroes = signal.Stage.GetHeroes();

            foreach(var hero in m_Heroes){
                hero.OnDamageTaken += OnHeroDamageTaken;
            }
        }
        
        private void FreePreviousBattleStage(){
            if(m_Heroes == null){return;}
            
            foreach(var hero in m_Heroes){
                hero.OnDamageTaken -= OnHeroDamageTaken;
            }
        }

        private void OnHeroDamageTaken(HeroView hero)
        {
            NotifyEnemyHealthChanged(hero);

            if (isHeroJustDied(hero))
            {
                hero.OnDamageTaken -= OnHeroDamageTaken;
                NotifyHeroDied(hero);
            }
        }

        private void NotifyHeroDied(HeroView hero) =>
            m_SignalBus.SendSignal(new HeroDiedSignal(hero));
        
        private void NotifyEnemyHealthChanged(HeroView enemy) =>
            m_SignalBus.SendSignal(new HeroHealthChangedSignal(enemy));

        private bool isHeroJustDied(HeroView hero) =>
            hero.GetState().GetCurrentHealth() == 0;
    }
}
