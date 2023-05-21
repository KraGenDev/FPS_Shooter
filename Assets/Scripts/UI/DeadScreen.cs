using System;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _killCounter;
    [SerializeField] private Button _restart;
    [SerializeField] private GameObject _screen;
    [SerializeField] private GameObject _ui;

    private int _killCount = 0;
    private void OnEnable()
    {
        PlayerHealth.Dead += ShowDeadScreen;
        Enemy.Enemy.Kill += AddKill;
        _restart.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        PlayerHealth.Dead -= ShowDeadScreen;
        Enemy.Enemy.Kill -= AddKill;

    }

    private void AddKill() => _killCount++;

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void ShowDeadScreen()
    {
        _screen.SetActive(true);
        _ui.SetActive(false);
        _killCounter.text = String.Format(_killCounter.text,_killCount);
    }
}
