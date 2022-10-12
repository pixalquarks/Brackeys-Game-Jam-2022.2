using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class  PauseMenu : MonoBehaviour
{
    public enum States 
    {
        Paused,
        Normal
    }
    public static States State { get; private set; }

    [SerializeField] private int mainMenuIndex;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Volume v;
    
    private void Awake()
    {
        State = States.Normal;
        pausePanel.SetActive(false);
    }

    private void OnDisable()
    {
        State = States.Normal;
        Time.timeScale = 1;
    }

    private void Update()
    {
        GetPauseInput();
    }

    public void PauseGame()
    {
        State = States.Paused;
    }

    public void ResumeGame()
    {
        State = States.Normal;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }

    public void Pause()
    {
        State = States.Paused;
        PauseMenuManager(State);
    }

    public void Resume()
    {
        State = States.Normal;
        PauseMenuManager(State);
    }

    private  void GetPauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Pause();
        }
    }
    
    private void PauseMenuManager(States state)
    {
        switch (state)
        {
            case States.Normal:
                pausePanel.SetActive(false);
                Time.timeScale = 1;
                break;
            case States.Paused:
                pausePanel.SetActive(true);
                Time.timeScale = 0;
                break;
            default:
                break;
        }
    }
    
    
}
