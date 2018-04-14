﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Main Menu")]
    public Button LevelSelect;
    
    [Header("Scene Menu UI")]
    public Text LevelName;
    public Text ScenePrimaryCollectibleNumber;
    public Text LevelPrimaryCollectibleNumber;
    public Text GamePrimaryCollectibleNumber;
    public Color DefaultImageColor;
    public Color CurrentlyChosenImageColor = Color.black;
    public Sprite[] LevelTextures;
    public CollectibleScore[] CollectibleScores;
    public Image[] LevelButtons;

    private GameObject _player;
    private PlayerController _playerController;

    private CameraController _camera;
    private bool _menuShowing = true;
    private GameObject _sceneMenu;
    private GameObject _mainMenu;
    private GameObject _ui;
    private GameObject _exitButton;
    
    private int _currentLevelSelected = 0;

    // we use Singleton Pattern
    public static Menu Instance;

    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DefaultImageColor = LevelButtons[0].color;
        HideMenu(_sceneMenu = GameObject.Find("SceneMenu"));
        _mainMenu = GameObject.Find("MainMenu");
        _exitButton = GameObject.Find("ExitButton");
        HideMenu(_ui = GameObject.Find("UI"));
        LevelButtons[_currentLevelSelected].color = CurrentlyChosenImageColor;
        LevelName.text = LevelTextures[_currentLevelSelected].name.Split('.')[0];
        LevelSelect.image.sprite = LevelTextures[_currentLevelSelected];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUIScores();
        if (Input.GetAxisRaw("Reload") == 1f && !_menuShowing)
        {
            SceneController.Instance.ReloadCurrentScene();
        }

        if (Input.GetAxisRaw("Pause") == 1f && !_menuShowing)
        {
            StopGame();
            ShowMenu(_mainMenu);
        }
    }

    void Start()
    {
        _camera = Camera.main.GetComponent<CameraController>();
        _camera.SetIdle(true);
        _player = GameObject.FindGameObjectWithTag("Player");
        (_playerController = _player.GetComponent<PlayerController>()).SetMobility(false);
        foreach (CollectibleScore cs in CollectibleScores)
        {
            cs.WipeScore();
        }
        UpdateMenuScores();
        UpdateUIScores();
    }

    public void UpdateMenuScores()
    {
        int totalCollectibles = 0;
        int currentCollectibles = 0;
        foreach (CollectibleScore cs in CollectibleScores)
        {
            foreach (bool isCollected in cs.GetPrimaryCollectedInStage())
            {
                if (isCollected)
                {
                    currentCollectibles++;
                }
            }
            totalCollectibles += cs.GetPrimaryCollectedInStage().Count;
        }
        GamePrimaryCollectibleNumber.text = currentCollectibles.ToString() +
            "/" +
            totalCollectibles.ToString();

        currentCollectibles = 0;
        foreach (bool isCollected in CollectibleScores[_currentLevelSelected + 1].GetPrimaryCollectedInStage())
        {
            if (isCollected)
            {
                currentCollectibles++;
            }
        }
        ScenePrimaryCollectibleNumber.text = currentCollectibles.ToString() +
            "/" +
            CollectibleScores[_currentLevelSelected + 1].GetPrimaryCollectedInStage().Count.ToString();
    }

    public void UpdateUIScores()
    {
        int currentCollectibles = 0;
        foreach (bool isCollected in SceneController.Instance._collectibleScore.GetPrimaryRecordInStage())
        {
            if (isCollected)
            {
                currentCollectibles++;
            }
        }
        LevelPrimaryCollectibleNumber.text = currentCollectibles +
            "/" +
            SceneController.Instance._collectibleScore.GetPrimaryCollectedInStage().Count.ToString();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        _camera.SetIdle(false);
        _playerController.SetMobility(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _exitButton.SetActive(false);
        _menuShowing = false;
        ShowMenu(_ui);
        UpdateUIScores();
    }

    public void StopGame()
    {
        _camera.SetIdle(true);
        _playerController.SetMobility(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _exitButton.SetActive(true);
        _menuShowing = true;
        HideMenu(_ui);
        UpdateMenuScores();
    }

    public void ShowMenu(GameObject menu)
    {
        if (menu == _sceneMenu)
        {
            ChangeLevelSelected(0);
        }
        menu.SetActive(true);
    }

    public void HideMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void SwapMenu(GameObject menuToAdd, GameObject menuToRemove)
    {
        menuToAdd.SetActive(true);
        menuToRemove.SetActive(false);
    }

    public void JumpToCurrentlySelectedLevel()
    {
        JumpToLevel(LevelTextures[_currentLevelSelected].name.Split('.')[0]);
    }

    public void JumpToLevel(string sceneName)
    {
        Camera.main.GetComponent<CameraController>().ResetCameraAngle();
        SceneController.Instance._collectibleScore.ResetScore();
        SceneController.Instance.LoadNewScene(sceneName);
        UpdateUIScores();
    }

    public void ChangeLevelSelected(int value)
    {
        LevelButtons[_currentLevelSelected].color = DefaultImageColor;
        _currentLevelSelected += value;
        if (_currentLevelSelected < 0)
            _currentLevelSelected += LevelTextures.Length;
        if (_currentLevelSelected >= LevelTextures.Length)
            _currentLevelSelected -= LevelTextures.Length;
        LevelButtons[_currentLevelSelected].color = CurrentlyChosenImageColor;
        LevelName.text = LevelTextures[_currentLevelSelected].name.Split('.')[0];
        LevelSelect.image.sprite = LevelTextures[_currentLevelSelected];
        UpdateMenuScores();
    }
}
