using System;
using System.Collections;
using System.Collections.Generic;
using pixalquarks.bgj2022_2;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public bool IsPlayerAlive = true;
    
    [SerializeField] private int mainMenuIndex;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject curtainPrefab;

    private GameObject _player;
    

    private void Awake()
    {
        deathPanel.SetActive(false);
        _player = GameObject.FindWithTag("Player");
        _player.GetComponent<Movement>().OnPlayerDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath(object sender, EventArgs e)
    {
        IsPlayerAlive = false;
        LoadDeathMenu();
        _player.GetComponent<Movement>().OnPlayerDeath -= OnPlayerDeath;
    }

    private void LoadDeathMenu()
    {
        deathPanel.SetActive(true);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
        AudioManager.i.StopDeathSound();
    }

    public void ReloadScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        AudioManager.i.StopDeathSound();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}