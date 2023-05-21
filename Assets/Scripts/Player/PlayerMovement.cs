using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;

        private const float _gravityForce = 9.81f;
        private CharacterController _characterController;
        private void Awake() => GetRequiredComponents();

        private void GetRequiredComponents() =>
            _characterController = gameObject.GetComponent<CharacterController>();

        public void Move(Vector3 axis)
        {
            
            var direction = Vector3.zero;

            if (_characterController.isGrounded)
            {
                direction += axis * _movementSpeed;
                direction = transform.TransformDirection(direction);
            }
            
            Gravity(ref direction.y);
        
            _characterController.Move(direction * Time.deltaTime);
        }

        public void MoveTo(Vector3 position) => 
            _characterController.Move(position - transform.position);
    
        private void Gravity(ref float directionY)
        {
            var isGround = _characterController.isGrounded;
            var gravity = isGround ? _gravityForce : _gravityForce * 10;
        
            directionY -= gravity * Time.deltaTime;
        }
    }
}
