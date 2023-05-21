using System;
using TMPro;
using UnityEngine;
using Zenject;

public class UserStatistics : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpCounter;
    [SerializeField] private TextMeshProUGUI _energyCounter;
    
    [Inject] private PlayerFacade _playerFacade;

    private void OnEnable()
    {
        PlayerFacade.OnValueChanged += UpdatePlayerStatistic;
    }

    private void OnDisable()
    {
        PlayerFacade.OnValueChanged -= UpdatePlayerStatistic;
    }

    private void Start()
    {
        UpdatePlayerStatistic();
    }

    private void UpdatePlayerStatistic()
    {
        var hp = _playerFacade.GetHealth();
        var energy = _playerFacade.GetEnergy();
        _hpCounter.text = ($"{hp}/100");
        _energyCounter.text = ($"{energy}/100");
    }
}
