using System;
using System.Collections;

using BakersDozenGames;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerBehavior : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private float musicTypeChangeTime = 1.0f;
    [SerializeField] private AudioSource menuMusicSource;
    [SerializeField] private AudioClip[] battleMusic;
    [SerializeField] private AudioSource battleMusicSource;
    [SerializeField] private AudioSource gameOver;
    [SerializeField] private AudioSource menuMove;
    [SerializeField] private AudioSource menuBack;
    [SerializeField] private AudioSource menuClick;

    public const string NormalMusicKey = "NormalMusic";
    public const string BattleMusicKey = "BattleMusic";
    public const string ChillMusicKey = "ChillMusic";

    public enum SFX
    {
        COMBOEXPLOSION,
        COMBOTRANSITION,
        ENEMYATTACKV1,
        ENEMYATTACKV2,
        ENEMYATTACKV3,
        ENEMYDEATHV1,
        ENEMYDEATHV2,
        ENEMYDEATHV3,
        ENEMYSPAWN,
        ENEMYDAMAGEV1,
        ENEMYDAMAGEV2,
        ENEMYDAMAGEV3,
        GAMEOVER,
        MENUMOVE,
        MENUBACK,
        MENUCLICK,
        MIRAIDEATH,
        MIRAIDAMAGE,
        MIRAITALK,
        SAMTALK,
        SLICEV1,
        SLICEV2,
        SLICEV3
    }

    public enum Music
    {
        Normal,
        Battle,
        Chill
    }

    public static SoundManagerBehavior Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
        // LoadVolumeSettings();
    }

    public void PlayMainMenuOST()
    {
        // var clip = Array.Find(menuMusic, music => music.name == "Intro").clip;
        // var length = clip.length;
        // menuMusicSource.clip = clip;
        // menuMusicSource.Play();
        // StartCoroutine(PlayMainLoop(length));
    }

    private IEnumerator PlayMainLoop(float delay)
    {
        yield return null;
        // yield return new WaitForSeconds(delay);
        // var clip = Array.Find(menuMusic, music => music.name == "Loop").clip;
        // menuMusicSource.clip = clip;
        // menuMusicSource.Play();
    }

    public void PlayBattleMusic(float delay = 0.0f)
    {
        StartCoroutine(BattleMusicLoop(delay));
    }

    public void StopBattleMusic()
    {
        // StopCoroutine(BattleMusicLoop());
        battleMusicSource.Stop();
    }

    private IEnumerator BattleMusicLoop(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        battleMusicSource.Play();
        // var counter = 0;
        // while (true)
        // {
        //     counter++;
        //     if (counter > battleMusic.Length)
        //     {
        //         counter = 0;
        //     }
        //     battleMusicSource.clip = battleMusic[counter];
        //     battleMusicSource.Play();
        //     yield return new WaitForSeconds(battleMusic[counter].length);
        // }
    }

    public void ChangeMusic(Music music, float delay = 1.0f)
    {
        StartCoroutine(ChangeMusicInternal(music, delay));
    }

    private IEnumerator ChangeMusicInternal(Music music, float delay = 1.0f)
    {
        yield return new WaitForSeconds(delay);
        switch (music)
        {
            case Music.Normal:
                ChangeVolume(normal: 1);
                break;
            case Music.Battle:
                ChangeVolume(battle: 1);
                break;
            case Music.Chill:
                ChangeVolume(chill : 1);
                break;
            default:
                break;
        }
    }
    
    public static float ConvertToDecibel(float volume)
    {
        var clamped = Mathf.Clamp(volume, 0.0001f, float.MaxValue);
        return Mathf.Log10(clamped) * 20.0f;
    }

    private void ChangeVolume(float normal = 0.001f, float battle = 0.001f, float chill = 0.001f)
    {
        normal = ConvertToDecibel(normal);
        battle = ConvertToDecibel(battle);
        chill = ConvertToDecibel(chill);
        masterMixer.SetFloat(NormalMusicKey, normal);
        masterMixer.SetFloat(BattleMusicKey, battle);
        masterMixer.SetFloat(ChillMusicKey, chill);
    }
    
}
