using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapon
{
    public class Bullet: MonoBehaviour
    {
        [SerializeField] protected int _damage = 15;
        [SerializeField] protected bool _canRebound = true;
        
        private PlayerFacade _playerFacade;
        private bool _rebound = false;

        public void SetDependency(PlayerFacade _facade) => _playerFacade = _facade;

        private void OnCollisionEnter(Collision other)
        {
            var isDamageable = TargetObject(other);
            if (isDamageable)
            {
                GetDamage(other);
                if(_canRebound) CheckRebound();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        protected virtual bool TargetObject(Collision other)
        {
            var isTarget = other.gameObject.TryGetComponent<IDamageable>(out var obj);
            return isTarget;
        }
        protected virtual void GetDamage(Collision other)
        {
            other.gameObject.GetComponent<IDamageable>()?.GetDamage(_damage);
        }
        
        private void CheckRebound()
        {
            var maxChance = 100 - _playerFacade.GetHealth();
            var chance = Random.Range(0, 101);
            if (chance <= maxChance)
            {
                if (_rebound)
                {
                    _rebound = false;
                    gameObject.SetActive(false);
                }
                else
                {
                    _rebound = true;
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}