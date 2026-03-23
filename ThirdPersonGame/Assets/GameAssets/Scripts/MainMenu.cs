using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _loadLevelButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private TMP_InputField _levelInput;
    [SerializeField] private Image _icorrectLevelIndicator;
    [SerializeField] private Image _levelLoadIndicator;
    [SerializeField] private Button _stopLoadingButton;

    private int _selectedLevel;


    private void OnEnable()
    {
        _stopLoadingButton.onClick.AddListener(OnStopLoadingClick);
        _loadLevelButton.onClick.AddListener(OnLoadLevelClick);
        _quitButton.onClick.AddListener(OnQuitClick);
        _levelInput.onValueChanged.AddListener(OnLevelSelected);
    }


    private void OnDisable()
    {
        _stopLoadingButton.onClick.RemoveListener(OnStopLoadingClick);
        _loadLevelButton.onClick.RemoveListener(OnLoadLevelClick);
        _quitButton.onClick.RemoveListener(OnQuitClick);
        _levelInput.onValueChanged.RemoveListener(OnLevelSelected);
    }

    private void OnStopLoadingClick()
    {
        LevelManager.StopLoading();
        _levelLoadIndicator.gameObject.SetActive(false);
    }

    private void Start()
    {
        _icorrectLevelIndicator.enabled = false;
    }


    private void OnLoadLevelClick()
    {
        if (LevelManager.IsLevelCorrect(_selectedLevel))
        {
            LoadLevel(_selectedLevel);
        }
        else
        {
            _loadLevelButton.enabled = true;
            Debug.Log($"Not Level{_selectedLevel}");
        }
    }


    private void OnQuitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        return;
#endif
        Application.Quit();
    }

    private void OnLevelSelected(string value)
    {
        _selectedLevel = int.Parse(value);
        if (LevelManager.IsLevelCorrect(_selectedLevel))
        {
            _icorrectLevelIndicator.enabled = false;
        }
    }

    private async void LoadLevel(int level)
    {
        _levelLoadIndicator.gameObject.SetActive(true); // к 16 дз добавть LoadBar
        await LevelManager.LoadAsync(level);
    }
}