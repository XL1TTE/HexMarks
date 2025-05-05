using Project.Actors.Stats;
using Project.Player;

namespace Project.EventBus.Signals{
    public class PlayerHealthChangedSingal : ISignal{
        private IHealthStatsAccessor m_HealthData;
        public float GetCurrentHealth() => m_HealthData.GetCurrentHealth();
        public float GetMaxHealth() => m_HealthData.GetMaxHealth();
        public PlayerHealthChangedSingal(IHealthStatsAccessor healthData){
            m_HealthData = healthData;
        }
    }
    
}
