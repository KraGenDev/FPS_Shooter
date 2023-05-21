using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Joystick _movementJoystick;
        [SerializeField] private Joystick _rotateJoystick;
        [Space]
        [SerializeField] private Button _shot;
        [SerializeField] private Button _ulta;

        private PlayerMovement _playerMovement;
        private PlayerRotate _playerRotate;
        private PlayerFacade _playerFacade;

        public static event Action OnShot;
        public static event Action OnUseUlta;
        private void Awake()
        {
            GetRequiredComponents();
            SetButtonListeners();
        }
        
        private void FixedUpdate() => GiveUserInput();

        private void GetRequiredComponents()
        {
            _playerMovement = gameObject.GetComponent<PlayerMovement>();
            _playerRotate = gameObject.GetComponent<PlayerRotate>();
            _playerFacade = gameObject.GetComponent<PlayerFacade>();
        }

        private void SetButtonListeners()
        {
            _shot.onClick.AddListener(Shot);
            _ulta.onClick.AddListener(Ulta);
        }
        private void GiveUserInput()
        {
            var moveDirection = Vector3.zero;
            var rotateDirection = Vector2.zero;

            moveDirection.x = _movementJoystick.Horizontal;
            moveDirection.z = _movementJoystick.Vertical;

            rotateDirection.x = _rotateJoystick.Horizontal;            
            rotateDirection.y = _rotateJoystick.Vertical;            
            
            _playerMovement.Move(moveDirection);
            _playerRotate.Rotate(rotateDirection);
        }

        private void Shot() => OnShot?.Invoke();
        private void Ulta()
        {
            var needEnergyForUlta = 100;
            Debug.Log("a");
            Debug.Log(_playerFacade.GetEnergy());
            if (_playerFacade.GetEnergy() != needEnergyForUlta) return;
            _playerFacade.SubtractEnergy(needEnergyForUlta);
            OnUseUlta?.Invoke();
        }
    }
}