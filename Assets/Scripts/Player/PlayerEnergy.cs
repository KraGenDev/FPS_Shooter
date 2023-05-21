using UnityEngine;

namespace Player
{
    public class PlayerEnergy : MonoBehaviour
    {
        [SerializeField] private int _startEnergy = 50;
        private int _energy;

        private void Awake() =>  _energy = _startEnergy;
        
        public int Energy() => _energy;

        public void SubtractEnergy(int damage)
        {
            _energy -= damage;
            _energy = Mathf.Clamp(_energy, 0, 100);
        }

        public void AddEnergy(int amount)
        {
            _energy += amount;
            _energy = Mathf.Clamp(_energy, 0, 100);
        }
    }
}