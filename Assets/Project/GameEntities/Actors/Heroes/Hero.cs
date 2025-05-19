using System;
using CMSystem;
using Project.EventBus;
using Project.EventBus.Signals;

namespace Project.Actors{
    public class Hero : ITargetable{
        public Hero(HeroView view, HeroState state, SignalBus signalBus){
            m_view = view;
            m_state = state;
            m_signalBus = signalBus;
            
            view.OnSelectedAsCardTarget += OnSelectedAsCardTarget;
        }

        public readonly HeroView m_view;
        private HeroState m_state;
        private readonly SignalBus m_signalBus;


        private void OnSelectedAsCardTarget()
        {
            //
        }
        
        public void RequestCardsDraw() =>
            m_signalBus.SendSignal(new RequestDrawCardsSignal(this, m_state.m_deck, m_state.m_stats.m_MaxCardsInHand, true));
        
        
        public string GetID() => m_state.m_id;
        public CMSEntity GetModel() => m_state.m_model;
        
        
        public int GetInitiative() => m_state.m_stats.m_BaseStats.m_Initiative;
        public float GetCurrentHealth() => m_state.m_stats.m_BaseStats.m_Health;
        public float GetMaximumHealth() => m_state.m_stats.m_BaseStats.m_MaxHealth;


        public float TakeDamage(float amount)
        {
            var temp = GetCurrentHealth();
            m_state.m_stats.m_BaseStats.m_Health = Math.Clamp(GetCurrentHealth() - amount, 0, GetMaximumHealth());

            var damage_taken = temp - GetCurrentHealth();

            NotifyHealthChanged();
            NotifyDamageTaken(damage_taken);
            
            return damage_taken;
        }
        public float TakeHeal(float amount)
        {
            var temp = GetCurrentHealth();
            m_state.m_stats.m_BaseStats.m_Health = Math.Clamp(GetCurrentHealth() + amount, 0, GetMaximumHealth());
            
            NotifyHealthChanged();

            return temp - GetCurrentHealth();
        }
        
        private void NotifyHealthChanged() => m_signalBus.SendSignal(new HeroHealthChangedSignal(this));
        private void NotifyDamageTaken(float amount) => m_signalBus.SendSignal(new HeroDamageTakenSignal(this, amount));
    }

}
