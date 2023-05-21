using UnityEngine;
using DG.Tweening;
using Player;

namespace Enemy
{
    public class RedEnemy : Enemy
    {
        [Header("Lifting Settings")]
        [SerializeField] private float _liftHeight;
        [SerializeField] private float _liftSpeed;
        [SerializeField] private float _delayAfterLifting;

        [Header("Attack Settings")] 
        [SerializeField] private float _flySpeed;
        [SerializeField] private int _damage;
        [Space] 
        [SerializeField] private int _rewardForKill = 15;
        

        private bool _attack = false;
        private PlayerFacade _playerFacade;

        public void SetTarget(Transform target)
        {
            _target = target;
            _playerFacade = target.GetComponent<PlayerFacade>();
        }

        private void OnEnable()
        {
            Move();
            PlayerInput.OnUseUlta += KillMe;
        }

        private void OnDisable()
        {
            _attack = false;
            transform.position = Vector3.zero;
            PlayerInput.OnUseUlta -= KillMe;
        }
        
        
        protected override void Move()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOMoveY(_liftHeight, _liftSpeed));
            sequence.AppendInterval(_delayAfterLifting);
            sequence.OnComplete((() => _attack = true));
        }

        protected override void FixedUpdate()
        {
            if (!_attack) return;
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _flySpeed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<PlayerFacade>(out var playerFacade))
            {
                playerFacade.Damage(_damage);
                gameObject.SetActive(false);
            }
        }
        
        protected override void GiveReward() => _playerFacade.AddEnergy(_rewardForKill);
    }
}