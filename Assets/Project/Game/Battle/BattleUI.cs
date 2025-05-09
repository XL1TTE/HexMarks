using System.Collections;
using Project.EventBus;
using Project.EventBus.Signals;
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

        private IEnumerator OnEnemyTurn(EnemyTurnSignal signal)
        {
            m_EndTurnButton.interactable = false;
            yield return null;
        }

        private IEnumerator OnPlayerTurn(PlayerTurnSignal signal)
        {
            m_EndTurnButton.interactable = true;
            yield return null;
        }

        [SerializeField] Slider m_HealthBar;
        [SerializeReference] Button m_EndTurnButton;
        public void AddEndTurnBtnOnClickListener(UnityAction listener){
            m_EndTurnButton.onClick.AddListener(listener);
        }
        public void RemoveEndTurnBtnOnClickListener(UnityAction listener){
            m_EndTurnButton.onClick.RemoveListener(listener);
        }
    
        private IEnumerator OnPlayerHealthChanged(PlayerHealthChangedSingal signal)
        {
            var maxHealth = signal.GetMaxHealth();
            var curHealth = signal.GetCurrentHealth();
            m_HealthBar.value = curHealth / maxHealth;
            
            yield return null;
        }
    }
}
