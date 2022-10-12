using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace pixalquarks.bgj2022_2
{
    public class VolumeManager : MonoBehaviour
    {
        // public Canvas mainCanvas;
        // public Canvas volumeCanvas;
        //
        // public GameObject mainButtons;
        public RectTransform configPanel;
        public Vector2 configPanelEndPos;
        public float slideInTime;
        
        public AudioMixer masterMixer;
        public Slider masterSlider;
        public Slider musicSlider;
        public Slider sfxSlider;

        public const string MasterVolumeKey = "MasterVolume";
        public const string MusicVolumeKey = "MusicVolume";
        public const string SfxVolumeKey = "SfxVolume";

        private bool volumeCanvasActive;

        private void Awake()
        {
            masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            sfxSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
        }

        private void Start()
        {
            LoadDefaultVolume();
        }

        private void LoadDefaultVolume()
        {
            masterSlider.value = PlayerPrefs.GetFloat(MasterVolumeKey, 1f);
            musicSlider.value = PlayerPrefs.GetFloat(MusicVolumeKey, 0.6f);
            sfxSlider.value = PlayerPrefs.GetFloat(SfxVolumeKey, 0.8f);
        }

        public void OnDisable()
        {
            PlayerPrefs.SetFloat(MasterVolumeKey, masterSlider.value);
            PlayerPrefs.SetFloat(MusicVolumeKey, musicSlider.value);
            PlayerPrefs.SetFloat(SfxVolumeKey, sfxSlider.value);
        }

        public void LoadLevelMenu()
        {
            SceneManager.LoadScene(1);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && volumeCanvasActive)
            {
                // SoundManagerBehavior.Instance.PlaySound(SoundManagerBehavior.SFX.MENUBACK);
                HideConfigMenu();
            }
        }

        public void playMenuClick()
        {
            // SoundManagerBehavior.Instance.PlaySound(SoundManagerBehavior.SFX.MENUCLICK);
        }

        public void ShowConfigMenu()
        {
            volumeCanvasActive = true;
            configPanel.DOAnchorPos(Vector2.zero, slideInTime).SetEase(Ease.Linear);
        }

        private void HideConfigMenu()
        {
            volumeCanvasActive = false;
            configPanel.DOAnchorPos(-configPanelEndPos, slideInTime).SetEase(Ease.Linear);
        }

        private void OnMasterVolumeChanged(float value)
        {
            masterMixer.SetFloat(MasterVolumeKey, AudioManager.ConvertToDecibel(value));
        }
        
        private void OnMusicVolumeChanged(float value)
        {
            masterMixer.SetFloat(MusicVolumeKey, AudioManager.ConvertToDecibel(value));
        }
        
        private void OnSfxVolumeChanged(float value)
        {
            masterMixer.SetFloat(SfxVolumeKey, AudioManager.ConvertToDecibel(value));
        }
    }
}
