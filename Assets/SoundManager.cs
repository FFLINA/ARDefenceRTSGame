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

    //public AudioClip bgmClip;

    //AudioSource audioBGM;
    List<AudioSource> effectPlayerList;
    int effectPlayerCount = 20;


    // Start is called before the first frame update
    void Start()
    {
        //audioBGM = gameObject.AddComponent<0Source>();

        effectPlayerList = new List<AudioSource>();
        for (int i = 0; i < effectPlayerCount; i++)
        {
            effectPlayerList.Add(gameObject.AddComponent<AudioSource>());
        }

        effectCilpDict = new Dictionary<EffectClipsEnum, AudioClip>();
        int len = (int)EffectClipsEnum.Length;
        for (int i = 0; i < len; i++)
        {
            //EffectClipsEnum c = (EffectClipsEnum)((int)EffectClipsEnum.arrowFire + i);
            EffectClipsEnum c = (EffectClipsEnum) i;
            // enum 의 이름과 파일의 이름이 같게 해서 바로 Dict에 Add가능
            AudioClip clip_item = Resources.Load<AudioClip>("Sound/" + c.ToString());
            effectCilpDict.Add(c, clip_item);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public enum BGM
    //{
    //    Lobby,
    //}
    //public void PlayBGM(BGM bgm)
    //{
    //    audioBGM.Stop();
    //    audioBGM.Play();
    //}
    public void PlayEffect(EffectClipsEnum clip, float volume)
    {
        for (int i=0; i< effectPlayerList.Count; i++)
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
