namespace Project.Player
{
    public abstract class IPlayerHealthData{
        public IPlayerHealthData(float health, float maxHealth)
        {
            m_Health = health;
            m_MaxHealth = maxHealth;
        }
        private float m_Health;
        private float m_MaxHealth;

        public virtual float GetMaxHealth() => m_MaxHealth;
        public virtual float GetCurrentHealth() => m_Health;
        public virtual float SetCurrentHealth(float value) => m_Health = value;
    }

    public class PlayerHealthData : IPlayerHealthData
    {
        public PlayerHealthData(float health, float maxHealth) : base(health, maxHealth){}
    }
}
