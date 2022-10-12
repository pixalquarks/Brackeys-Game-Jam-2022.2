using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace pixalquarks.bgj2022_2
{
    public class VictoryScreen : MonoBehaviour
    {
        [SerializeField] private  GameObject victoryScreen;
        [SerializeField] private Barrier finalBarrier;

        private void Awake()
        {
            finalBarrier.OnLevelWin += OnLevelWin;
            victoryScreen.SetActive(false);
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }

        private void OnLevelWin(object sender, EventArgs e)
        {
            PlayerPrefs.SetInt(LevelManagers.MaxUnlockedLevel, SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 0;
            victoryScreen.SetActive(true);
        }

        public void LoadNext()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void LoadLevels()
        {
            SceneManager.LoadScene(1);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
