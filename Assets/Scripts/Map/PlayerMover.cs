using System;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public class PlayerMover : MonoBehaviour
    {        
        [SerializeField] private float _planeWidth = 30f;
        [SerializeField] private float _planeLength = 30f;

        public static event Action PlayerTeleported;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
            {
                
                var position = Vector3.zero;
                position.x = Random.Range(-_planeLength,_planeLength);
                position.z = Random.Range(-_planeWidth,_planeWidth);
                position.y = 1f;

                playerMovement.MoveTo(position);
                PlayerTeleported?.Invoke();
            }
        }
    }
}