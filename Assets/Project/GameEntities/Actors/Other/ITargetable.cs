namespace Project.Actors{
    
    public interface ITargetable{
        
        public float GetCurrentHealth();
        
        public float TakeDamage(float amount);
        public float TakeHeal(float amount);
    }
    
}
