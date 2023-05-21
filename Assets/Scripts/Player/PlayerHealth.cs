using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int _startHealth = 100;
        private int _health;

        public static event Action Dead;
        
        private void Awake() =>  _health = _startHealth;

        public int Health() => _health;

        public void GetDamage(int damage)
        {
            _health -= damage;
            _health = Mathf.Clamp(_health, 0, 100);
            if (_health <= 0)
            {
                Dead?.Invoke();
            }
        }
        public void AddHealth(int amount)
        {
            _health += amount;
            _health = Mathf.Clamp(_health, 0, 100);
        }
    }
}