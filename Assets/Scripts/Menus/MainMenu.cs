using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public GameObject SettingsMenu;
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ComeSettingsMenu()
    {
        SettingsMenu.SetActive(true);
    }

    public void LeaveSettingsMenu()
    {
        SettingsMenu.SetActive(false);
    }

    public void CloseTheGame()
    {
        Application.Quit();
    }
}
