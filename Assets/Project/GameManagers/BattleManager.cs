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
        [SerializeField] BattleTurnsController m_BattleTurnsController;


        void OnEnable()
        {
            m_EnemiesController.AddEnemyDieValidator(EnemyDiedValidator);
            m_BattleTurnsController.AddNextTurnValidator(NextTurnValidator);
        }

        void OnDisable()
        {
            m_EnemiesController.RemoveEnemyDieValidator(EnemyDiedValidator);
            m_BattleTurnsController.RemoveNextTurnValidator(NextTurnValidator);
        }


        private bool EnemyDiedValidator()
        {
            return !m_CardsController.IsAnyCardExecuting();
        }
        
        private bool NextTurnValidator(){
            return !m_CardsController.IsAnyCardExecuting();
        }
    }
}
