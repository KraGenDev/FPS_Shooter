using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class BlueEnemy : Enemy
    {
        [SerializeField] private float _minDistanceToTarget = 20;
        [SerializeField] private float _shotDelay = 3;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private EnemyWeapon _weapon;
        [Space] 
        [SerializeField] private int _rewardForKill = 50;
        protected NavMeshAgent _agent;
        
        private bool _attack = false;
        private PlayerFacade _playerFacade;
        
        protected override void Awake()
        {
            base.Awake();
            GetRequiredComponents();
        }

        public void SetPlayer(PlayerFacade _playerFacade,Transform transform)
        {
            _weapon.SetPlayerFacade(_playerFacade);
            this._playerFacade = _playerFacade;
            _target = transform;
        }
        
        private void OnEnable()
        {
            _agent.speed = _movementSpeed;
            PlayerInput.OnUseUlta += KillMe;
        }

        private void OnDisable()
        {
            PlayerInput.OnUseUlta -= KillMe;
        }

        protected virtual void GetRequiredComponents()
        {
            _agent = gameObject.GetComponent<NavMeshAgent>();
        }

        protected override void Move()
        {
            if (_target != null)
            {
                var distance = Vector3.Distance(transform.position, _target.position);

                if (distance > _minDistanceToTarget)
                {
                    _attack = false;
                    _agent.SetDestination(_target.position);
                    StopCoroutine(AttackCoroutine());
                }
                else
                {
                    if(!_attack) StartCoroutine(AttackCoroutine());
                    _agent.SetDestination(transform.position);
                }
            }
        }

        private IEnumerator AttackCoroutine()
        {
            _attack = true;
            while (_attack)
            {
                yield return new WaitForSeconds(_shotDelay);
                Attack();
            }
        }

        protected override void Attack() => _weapon.Shot();
        protected override void GiveReward() => _playerFacade.AddEnergy(_rewardForKill);

    }
}