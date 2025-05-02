using System.Collections;
using System.Collections.Generic;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Game.Battle.UI
{
    public class BattleUI : MonoBehaviour
    {        
        [Inject]
        private void Construct(SignalBus signalBus){
            signalBus.Subscribe<PlayerHealthChangedSingal>(OnPlayerHealthChanged);
        }

        [SerializeField] Slider m_HealthBar;
        
        
        private void OnPlayerHealthChanged(PlayerHealthChangedSingal signal)
        {
            var maxHealth = signal.GetMaxHealth();
            var curHealth = signal.GetCurrentHealth();
            m_HealthBar.value = curHealth / maxHealth;
        }
    }
}
