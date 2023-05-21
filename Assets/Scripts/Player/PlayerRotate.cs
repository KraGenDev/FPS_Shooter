using UnityEngine;

namespace Player
{
    public class PlayerRotate : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;

        private float _vertical;

        public void Rotate(Vector2 direction)
        {
            _vertical += direction.y * rotateSpeed;
            _vertical = Mathf.Clamp(_vertical, minAngle, maxAngle);

            transform.localRotation = Quaternion.Euler(-_vertical, transform.localEulerAngles.y, 0);
            transform.Rotate(Vector3.up, direction.x * rotateSpeed);
        }
    }
}