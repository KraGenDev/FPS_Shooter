using Map;
using UnityEngine;

namespace Weapon
{
    public class Rocket : Bullet
    {
        [SerializeField] private float _flyingSpeed;
        
        private Transform _target;
        private bool _targetTeleported = false;
        private Vector3 _targetPosition;
        
        private void OnEnable()
        {
            PlayerMover.PlayerTeleported += PlayerTeleported;
        }

        private void OnDisable()
        {
            PlayerMover.PlayerTeleported -= PlayerTeleported;
            _targetTeleported = false;
        }

        
        private void PlayerTeleported() => _targetTeleported = true;

        public void SetTarget(Transform target) => _target = target;

        private void FixedUpdate() => FlyToTarget();

        private void FlyToTarget()
        {
            if(!_targetTeleported && _target) _targetPosition = _target.position;

            transform.position =
                Vector3.MoveTowards(transform.position, _targetPosition, _flyingSpeed * Time.deltaTime);
            transform.LookAt(_targetPosition);
        }

        protected override bool TargetObject(Collision other)
        {
            var isTarget = other.gameObject.TryGetComponent<PlayerFacade>(out var obj);
            return isTarget;
        }

        protected override void GetDamage(Collision other) =>
            other.gameObject.GetComponent<PlayerFacade>().SubtractEnergy(_damage);
    }
}