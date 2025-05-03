using System;
using System.Collections;
using System.Collections.Generic;
using Project.Cards;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.Factories;
using Project.Game.Battle.UI;
using Project.Game.GameLevels;
using Project.Layouts;
using Project.Player;
using Project.UI;
using Project.Utilities.Extantions;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Project.Game.Battle{
    
    public class BattleController : MonoBehaviour
    {
        [Inject]
        private void Construct(
            SignalBus signalBus, 
            IEnemyViewFactory enemyViewFactory)
        {
            m_enemyViewFactory = enemyViewFactory;
            m_SignalBus = signalBus;
        }

        [SerializeField] private List<GameLevel> m_Levels;
        [SerializeField] private List<Transform> m_EnemySpawnPoints;
        
        [SerializeField] private GameObject WinMessage;
        
        private IEnemyViewFactory m_enemyViewFactory;
        private SignalBus m_SignalBus;
        
        void Awake()
        {
            WinMessage.SetActive(false);
        }

        public void Initialize(){
            m_SignalBus.Subscribe<PlayerWonBattleSignal>(OnPlayerWon);
            m_SignalBus.Subscribe<PlayerLostBattleSignal>(OnPlayerLost);
        }

        public void StartBattle(){
            NextLevel();
        }

        private void NextLevel(){
            if(m_Levels.Count == 0){return;}

            List<EnemyView> m_EnemiesInBattle = new();
            
            var level = m_Levels.Dequeue();
            var enemies = level.GetEnemies();
            
            int index = 0;
            foreach (var p in m_EnemySpawnPoints){
                
                var enemy = m_enemyViewFactory.CreateFromDef(enemies[index], p);
                m_EnemiesInBattle.Add(enemy);

                m_SignalBus.SendSignal(new EnemySpawnedSignal(enemy));

                if (++index >= enemies.Count){break;}
            }
            m_SignalBus.SendSignal(new BattleStartSignal(m_EnemiesInBattle));
        }

        private void OnPlayerWon(PlayerWonBattleSignal signal)
        {
            StartCoroutine(OnLevelComplete());
        }
        private void OnPlayerLost(PlayerLostBattleSignal signal){
            StartCoroutine(OnPlayerLostRoutine());
        }

        private IEnumerator OnLevelComplete(){
            
            yield return UINotification.current.ShowNotification("You won!", 2.0f, "#46EE59".ToColor());

            NextLevel();
        }
        
        private IEnumerator OnPlayerLostRoutine(){
            yield return UINotification.current.ShowNotification("You lost!", 2.0f, "#751B13".ToColor());
        }
    }

}
