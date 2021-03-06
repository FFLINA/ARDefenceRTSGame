﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*  게임 시작 시 스테이지 1
     *  게임 시작 버튼을 누르면 스테이지 시작
     *  스테이지 시작 시 게임 시작 버튼은 사라짐
     *  스테이지는 UI로 표시
     *  다음 스테이지
     *  게임 시작 버튼이 다시 나타남
     * 
     */
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public Text stageText;
    public Text totalCountText;
    int stage;

    public bool hasCrystal { get; set; }
    public bool GameStart { get; set; }
    int totalCount;
    public int TotalCount 
    { 
        get { return totalCount; }
        set
        {
            totalCount = value;
            totalCountText.text = totalCount.ToString();
        }
    }

    public Button gameStartButton;

    GameObject gameFieldsFactory;
    GameObject gameFields;

    GameObject crystalFactory;
    GameObject crystal;


    // Start is called before the first frame update
    void Start()
    {
        hasCrystal = false;
        GameStart = false;

        stage = 0;
        stageText.text = stage.ToString("00");

        crystalFactory = Resources.Load<GameObject>("MainCrystal");
        gameFieldsFactory = Resources.Load<GameObject>("GameFields");

        gameStartButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(hasCrystal == false)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mos = Input.mousePosition;
                // 카메라가 보는 방향과, 시야를 가져온다.
                mos.z = Camera.main.farClipPlane;

                Vector3 dir = Camera.main.ScreenToWorldPoint(mos);
                // 월드의 좌표를 클릭했을 때 화면에 자신이 보고있는 화면에 맞춰 좌표를 바꿔준다.

                // 레이를 쏴서
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, dir, out hit, mos.z))
                {
                    // 닿은 곳의 태그가 땅이면
                    if (hit.transform.CompareTag("Floor"))
                    {
                        CreateGameField(hit);
                        hasCrystal = true;
                        gameStartButton.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
    private void CreateGameField(RaycastHit hit)
    {
        if (gameFields == null)
        {
            // 게임필드를 만들고
            gameFields = Instantiate(gameFieldsFactory);
            gameFields.transform.position = hit.point;
            Vector3 offset = new Vector3(0, hit.transform.localScale.y / 2, 0);
            crystal = Instantiate(crystalFactory);
            crystal.transform.position = hit.point + offset;
            // 크리스탈 포지션 저장
            BuildManager.Instance.CrystalPosition = crystal.transform.position;
            // 에너미매니저한테 게임필드 생성됐다고 알림
            EnemyManager.Instance.SetGameField(gameFields);
            EnemyManager.Instance.SetCrystal(crystal);
        }
    }

    public void OnClickStart()
    {
        GameStart = true;
        gameStartButton.gameObject.SetActive(false);
        //gameStartButton.enabled = false;
        stage++;
        stageText.text = stage.ToString("00");
        EnemyManager.Instance.SetNextStage(stage);
    }

    public void FinishStage()
    {
        GameStart = false;
        //gameStartButton.enabled = true;
        gameStartButton.gameObject.SetActive(true);
    }
}
