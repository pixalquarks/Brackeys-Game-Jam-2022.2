using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace pixalquarks.bgj2022_2
{
    public enum Sfx
    {
        LeverPull,
        GateOpen,
        KeyPickup,
        
    }
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager i;
        public SfxClip[] sfxClips;

        private AudioSource _audioSource;
        
        [SerializeField] private AudioMixer masterMixer;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private float musicTypeChangeTime = 1.0f;
        [SerializeField] private AudioClip[] battleMusic;
        [SerializeField] private AudioSource gameOver;
        [SerializeField] private AudioSource enemyDetect;

        private int _enemyCount = 0;

        private void Awake()
        {
            if (i == null)
            {
                i = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
            _audioSource = GetComponent<AudioSource>();
            LoadVolumeSettings();
            
            if (gameOver.isPlaying)
            {
                gameOver.Stop();
            }
            
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }

        public void PlayOnce(Sfx sfx)
        {
            switch (sfx)
            {
                case Sfx.LeverPull:
                    sfxSource.PlayOneShot(Array.Find(sfxClips, sfxClip => sfxClip.name == "LeverPull").clip);
                    break;
                case Sfx.GateOpen:
                    sfxSource.PlayOneShot(Array.Find(sfxClips, sfxClip => sfxClip.name == "GateOpen").clip);
                    break;
                case Sfx.KeyPickup:
                    sfxSource.PlayOneShot(Array.Find(sfxClips, sfxClip => sfxClip.name == "KeyPickup").clip);
                    break;
                default:
                    break;
            }
        }

        public void PlayEnemyDetect()
        {
            _enemyCount++;
            if (_enemyCount > 1) return;
            enemyDetect.Play();
        }
        
        public void StopEnemyDetect()
        {
            _enemyCount--;
            if (_enemyCount > 0) return;
            enemyDetect.Stop();
            enemyDetect.PlayOneShot(Array.Find(sfxClips, sfxClip => sfxClip.name == "EnemyDetectFade").clip);
        }

        public void StopOnPlayerDeath()
        {
            Debug.Log("Player dead");
            enemyDetect.Stop();
            musicSource.Stop();
            gameOver.Play();
        }

        public void StopDeathSound()
        {
            gameOver.Stop();
        }

        private void LoadVolumeSettings()
        {
            var masterVolume = PlayerPrefs.GetFloat(VolumeManager.MasterVolumeKey, 1.0f);
            var musicVolume = PlayerPrefs.GetFloat(VolumeManager.MusicVolumeKey, 0.6f);
            var sfxVolume = PlayerPrefs.GetFloat(VolumeManager.SfxVolumeKey, 0.8f);

            masterMixer.SetFloat(VolumeManager.MasterVolumeKey, ConvertToDecibel(masterVolume));
            masterMixer.SetFloat(VolumeManager.MusicVolumeKey, ConvertToDecibel(musicVolume));
            masterMixer.SetFloat(VolumeManager.SfxVolumeKey, ConvertToDecibel(sfxVolume));
        }
        
        public static float ConvertToDecibel(float volume)
        {
            var clamped = Mathf.Clamp(volume, 0.0001f, float.MaxValue);
            return Mathf.Log10(clamped) * 20.0f;
        }
    }
}
