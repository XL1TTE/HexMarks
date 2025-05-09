using System;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Game.Battle.Controllers;
using UnityEngine;
using Zenject;

namespace Project.GameManagers{
    public class BattleManager: MonoBehaviour{
        [Inject]
        private void Construct(SignalBus signalBus){
            m_SignalBus = signalBus;
        }
        private SignalBus m_SignalBus;
        
        [SerializeField] EnemiesInBattleController m_EnemiesController;
        [SerializeField] CardsExecutionController m_CardsController;


        void OnEnable()
        {
            m_EnemiesController.AddEnemyDieValidator(EnemyDiedValidator);
        }

        void OnDisable()
        {
            m_EnemiesController.RemoveEnemyDieValidator(EnemyDiedValidator);
        }


        private bool EnemyDiedValidator()
        {
            var result = !m_CardsController.IsAnyCardExecuting();
            return result;
        }
    }
}
