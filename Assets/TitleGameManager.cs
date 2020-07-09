using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SoundManager;

public class TitleGameManager : MonoBehaviour
{
    public GameObject BG_1920;
    public GameObject BG_1080;
    // Start is called before the first frame update
    void Start()
    {
        BGMEnum bgm = BGMEnum.BGM_Title;
        SoundManager.Instance.PlayBGM(bgm, 0.3f);

        BG_1920.SetActive(false);
        BG_1080.SetActive(false);

        int i_width = Screen.width;

        int i_height = Screen.height;

        if(i_width > i_height)
        {
            BG_1920.SetActive(true);
        }
        else
        {
            BG_1080.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickGameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit() // 어플리케이션 종료
#endif
    }
}
