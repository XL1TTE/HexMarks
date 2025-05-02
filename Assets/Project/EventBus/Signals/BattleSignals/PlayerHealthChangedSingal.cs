using Project.Player;

namespace Project.EventBus.Signals{
    public class PlayerHealthChangedSingal : ISignal{
        private IPlayerHealthData m_HealthData;
        public float GetCurrentHealth() => m_HealthData.GetCurrentHealth();
        public float GetMaxHealth() => m_HealthData.GetMaxHealth();
        public PlayerHealthChangedSingal(IPlayerHealthData healthData){
            m_HealthData = healthData;
        }
    }
    
}
