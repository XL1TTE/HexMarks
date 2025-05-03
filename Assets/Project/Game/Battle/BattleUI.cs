using System;
using System.Collections;
using System.Collections.Generic;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Project.Game.Battle.UI
{
    public class BattleUI : MonoBehaviour
    {        
        [Inject]
        private void Construct(SignalBus signalBus){
            signalBus.Subscribe<PlayerHealthChangedSingal>(OnPlayerHealthChanged);
            signalBus.Subscribe<PlayerTurnSignal>(OnPlayerTurn);
            signalBus.Subscribe<EnemyTurnSignal>(OnEnemyTurn);
        }

        private void OnEnemyTurn(EnemyTurnSignal signal)
        {
            m_EndTurnButton.interactable = false;
        }

        private void OnPlayerTurn(PlayerTurnSignal signal)
        {
            m_EndTurnButton.interactable = true;
        }

        [SerializeField] Slider m_HealthBar;
        [SerializeReference] Button m_EndTurnButton;
        public void AddEndTurnBtnOnClickListener(UnityAction listener){
            m_EndTurnButton.onClick.AddListener(listener);
        }
        public void RemoveEndTurnBtnOnClickListener(UnityAction listener){
            m_EndTurnButton.onClick.RemoveListener(listener);
        }
    
        private void OnPlayerHealthChanged(PlayerHealthChangedSingal signal)
        {
            var maxHealth = signal.GetMaxHealth();
            var curHealth = signal.GetCurrentHealth();
            m_HealthBar.value = curHealth / maxHealth;
        }
    }
}
