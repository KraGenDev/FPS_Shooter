using System;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _maxHp;
        private EnemyHealth _health;
        protected Transform _target;

        public static event Action Kill;
        protected virtual void Awake() => SetHealth();
        protected virtual void FixedUpdate() => Move();

        public void SetHealth()
        {
            _health = new EnemyHealth();
            _health.SetHealth(_maxHp);
        }
        
        protected void KillMe() => gameObject.SetActive(false);
        protected virtual void Move(){}
        protected virtual void Attack(){}

        private void SetTarget(Transform target) => _target = target;
        public virtual void GetDamage(int amount)
        {
            _health.GetDamage(amount);
            var isDead = _health.Dead();
            if (isDead)
            {
                KillMe();
                GiveReward();
                Kill?.Invoke();
            }
        }

        protected virtual void GiveReward() { }
    }
}