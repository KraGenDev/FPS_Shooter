using UnityEngine;
using Weapon;

namespace Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject _weapon;

        private void OnEnable() => PlayerInput.OnShot += Shot;

        private void OnDisable() => PlayerInput.OnShot -= Shot;

        private void Shot() => _weapon.GetComponent<IGun>()?.Shot();
    }
}