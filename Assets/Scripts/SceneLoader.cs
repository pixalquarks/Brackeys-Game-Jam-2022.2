using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BakersDozenGames
{
    public class SceneLoader : MonoBehaviour
    {
        public int mainMenuSceneIndex = 0;
        public int tutorialSceneIndex = 1;
        public int gameSceneIndex = 2;
        public Animator transition;
        public float transitionTime = 1f;
        private static readonly int FadeIn = Animator.StringToHash("FadeIn");

        // public static SceneLoader Instance;
        //
        // private void Awake()
        // {
        //     if (Instance == null)
        //     {
        //         Instance = this;
        //         DontDestroyOnLoad(gameObject);
        //     }
        //     else
        //     {
        //         Destroy(gameObject);
        //     }
        // }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            if (SceneManager.GetActiveScene().buildIndex == mainMenuSceneIndex)
            {
                SoundManagerBehavior.Instance.PlayMainMenuOST();
            }
        }


        public void Play()
        {
            StartCoroutine(LoadScene(tutorialSceneIndex));
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().buildIndex == tutorialSceneIndex)
            {
                LoadGameScene();
            }
        }

        public void LoadGameScene()
        {
            // SoundManagerBehavior.Instance.PlaySound(SoundManagerBehavior.SFX.MENUCLICK);
            SoundManagerBehavior.Instance.ChangeMusic(SoundManagerBehavior.Music.Battle, 3f);
            SoundManagerBehavior.Instance.PlayBattleMusic(3f);
            StartCoroutine(LoadScene(gameSceneIndex));
        }

        public void Conf()
        {
            print("Loading Conf");
        }

        public void PauseMenu()
        {

        }

        public void MainMenu()
        {
            StartCoroutine(LoadScene(mainMenuSceneIndex));
        }
        
        public void Exit()
        {
            Application.Quit();
        }

        private IEnumerator LoadScene(int sceneIndex)
        {
            transition.SetTrigger(FadeIn);
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
