namespace Enemy
{
    public class EnemyHealth
    {
        private int _health;

        public void SetHealth(int amount) => _health = amount;
        public void AddHealth(int amount) => _health += amount;
        public void GetDamage(int amount) => _health -= amount;
        public bool Dead() => _health <= 0;
    }
}