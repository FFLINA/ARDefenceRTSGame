using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 소리를 내는 기관 : AudioSource
// 소리파일 : AudioClip
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private void Awake()
    {
        Instance = this;
        InitSound();
    }

    private void InitSound()
    {
        // 배경음악
        bgmPlayer = gameObject.AddComponent<AudioSource>();

        bgmClipDict = new Dictionary<BGMEnum, AudioClip>();
        int len = (int)BGMEnum.Length;
        for (int i = 0; i < len; i++)
        {
            BGMEnum c = (BGMEnum)i;
            AudioClip clip_item = Resources.Load<AudioClip>("Sound/" + c.ToString());
            bgmClipDict.Add(c, clip_item);
        }

        // 이펙트 사운드

        effectPlayerList = new List<AudioSource>();
        for (int i = 0; i < effectPlayerCount; i++)
        {
            effectPlayerList.Add(gameObject.AddComponent<AudioSource>());
        }

        effectCilpDict = new Dictionary<EffectClipsEnum, AudioClip>();
        len = (int)EffectClipsEnum.Length;
        for (int i = 0; i < len; i++)
        {
            //EffectClipsEnum c = (EffectClipsEnum)((int)EffectClipsEnum.arrowFire + i);
            EffectClipsEnum c = (EffectClipsEnum)i;
            // enum 의 이름과 파일의 이름이 같게 해서 바로 Dict에 Add가능
            AudioClip clip_item = Resources.Load<AudioClip>("Sound/" + c.ToString());
            effectCilpDict.Add(c, clip_item);
        }
    }

    public Dictionary<EffectClipsEnum, AudioClip> effectCilpDict;

    public enum EffectClipsEnum
    {
        ArrowFire,
        CanonExplosion,
        CanonFire,
        CrystalBuild,
        CrystalDestroy,
        EnemyGolemAttack,
        EnemyGolemDeath,
        EnemyWolfAttack,
        EnemyWolfDeath,
        EnemyWoodAttack,
        EnemyWoodDeath,
        StageClear,
        TowerBuild,
        TowerDestroy,
        TowerSell,
        Length,
    }

    AudioClip bgmClip;

    AudioSource bgmPlayer;

    public Dictionary<BGMEnum, AudioClip> bgmClipDict;

    public enum BGMEnum
    {
        BGM_Title, BGM_Game, BGM_Boss, BGM_GameOver, Length
    }


    List<AudioSource> effectPlayerList;
    int effectPlayerCount = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }
    public void PlayBGM(BGMEnum bgm)
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = bgmClipDict[bgm];
        bgmPlayer.loop = true;
        bgmPlayer.Play();
    }
    public void PlayBGM(BGMEnum bgm, float volume)
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = bgmClipDict[bgm];
        bgmPlayer.volume = volume;
        bgmPlayer.loop = true;
        bgmPlayer.Play();
    }


    public void PlayEffect(EffectClipsEnum clip, float volume)
    {
        for (int i = 0; i < effectPlayerList.Count; i++)
        {
            if (effectPlayerList[i].isPlaying == false)
            {
                effectPlayerList[i].Stop();
                effectPlayerList[i].clip = effectCilpDict[clip];
                effectPlayerList[i].volume = volume;
                effectPlayerList[i].Play();
                return;
            }
        }
    }

    public void PlayEffect(EffectClipsEnum clip)
    {
        for (int i = 0; i < effectPlayerList.Count; i++)
        {
            if (effectPlayerList[i].isPlaying == false)
            {
                effectPlayerList[i].Stop();
                effectPlayerList[i].clip = effectCilpDict[clip];
                effectPlayerList[i].Play();
                return;
            }
        }
    }


}
