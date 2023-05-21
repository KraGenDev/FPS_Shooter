using System;
using Player;
using UnityEngine;

public class PlayerFacade : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerEnergy _playerEnergy;

    public static event Action OnValueChanged;
    public int GetHealth() => _playerHealth.Health();
    
    public void AddHealth(int amount)
    {
        _playerHealth.AddHealth(amount);
        OnValueChanged?.Invoke();
    } 
    public void Damage(int damage)
    {
        _playerHealth.GetDamage(damage);
        OnValueChanged?.Invoke();
    }

    public int GetEnergy() => _playerEnergy.Energy();
    
    public void AddEnergy(int amount) 
    {
        _playerEnergy.AddEnergy(amount);
        OnValueChanged?.Invoke();
    }
    public void SubtractEnergy(int amount)
    {
        _playerEnergy.SubtractEnergy(amount);
        OnValueChanged?.Invoke();
    }
}
