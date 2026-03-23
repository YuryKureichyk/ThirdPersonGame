using System;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    private int _scoreCoins = 0;


    private void OnEnable()
    {
        Coin.OnCoinCollected += AddScore;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= AddScore;
    }

    private void Start()
    {
        UpdateUI();
    }


    private void AddScore()
    {
        _scoreCoins++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_score != null)
            _score.text = _scoreCoins.ToString();
    }
}