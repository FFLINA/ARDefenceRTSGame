﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /*  스테이지 별 에너미 수 만큼 에너미 생성
     *  일반 = 89퍼 엘리트 = 10퍼 보스 = 1퍼
     *  남은 에너미 수 UI로 표시
     *  남은 에너미 수가 0이 되면 스테이지 
     * 
     */
    public static EnemyManager Instance;
    private void Awake()
    {
        Instance = this;
        hasGameField = false;
    }

    int currentStage;
    // 게임필드가 생성됐을 때
    bool hasGameField;
    // 에너미 필드 배열을 받아옴
    List<GameObject> enemyFields = new List<GameObject>();

    // 임시
    // 2초마다 에너미 생성
    // 프리팹 공장
    GameObject enemyNormalFactory;
    GameObject enemyEliteFactory;
    GameObject enemyBossFactory;
    int normalCount, eliteCount, bossCount;
    int normalMaxCount, eliteMaxCount, bossMaxCount;
    int totalCount;

    // 생성시간
    float respawnNormal;
    float respawnElite;
    float respawnBoss;

    // Start is called before the first frame update
    void Start()
    {
        currentStage = 0;

        respawnNormal = 0.5f;
        respawnElite = respawnNormal * 5;
        respawnBoss = respawnElite * 5;

        enemyNormalFactory = Resources.Load<GameObject>("Enemy_Normal");
        enemyEliteFactory = Resources.Load<GameObject>("Enemy_Elite");
        enemyBossFactory = Resources.Load<GameObject>("Enemy_Boss");
    }

    // 일반 적 0.5초마다 생산
    // 일반 적이 절반이상 생성됐을 시 엘리트 적 생산 시작
    // 엘리트 적은 일반적 생산시간의 5배
    // 엘리트 적이 절반이상 생성됐을 시 보스 적 생산
    // 보스 적은 엘리트 적 생산시간의 5배

    float tempNormal = 0f;
    float tempElite = 0f;
    float tempBoss = 0f;

    void Update()
    {
        if (GameManager.Instance.GameStart == true)
        {
            tempNormal += Time.deltaTime;
            if (tempNormal >= respawnNormal)
            {
                if (normalCount < normalMaxCount)
                {
                    // 노말 적 생산
                    CreateEnemy(enemyNormalFactory);
                    print("normalCount : " + normalCount);
                    normalCount++;
                    GameManager.Instance.TotalCount--;
                    tempNormal = 0;
                }
            }
            if (normalCount > normalMaxCount / 2)
            {
                tempElite += Time.deltaTime;
                if (tempElite >= respawnElite && eliteCount < eliteMaxCount)
                {
                    // 엘리트 적 생산
                    CreateEnemy(enemyEliteFactory);
                    print("eliteCount : " + eliteCount);
                    eliteCount++;
                    GameManager.Instance.TotalCount--;
                    tempElite = 0;
                }
            }
            if (eliteCount > eliteMaxCount / 2)
            {
                tempBoss += Time.deltaTime;
                if (tempBoss >= respawnBoss && bossCount < bossMaxCount)
                {
                    // boss 적 생산
                    CreateEnemy(enemyBossFactory);
                    print("bossCount : " + bossCount);
                    bossCount++;
                    GameManager.Instance.TotalCount--;
                    tempBoss = 0;
                }
            }
            if (GameManager.Instance.TotalCount <= 0)
            {
                // 임시 - 
                // TODO : 마지막몹이 죽을 때 스테이지 클리어로 해야함
                GameManager.Instance.FinishStage();
                normalCount = 0;
                eliteCount = 0;
                bossCount = 0;
                tempNormal = 0;
                tempElite = 0;
                tempBoss = 0;
            }
        }
    }

    private void CreateEnemy(GameObject enemyFactory)
    {
        GameObject enemy;
        enemy = Instantiate(enemyFactory);
        Vector3 offset = new Vector3(0, enemyFields[0].transform.localScale.y, 0) / 2;
        int ranValue = UnityEngine.Random.Range(0, enemyFields.Count);
        enemy.transform.position = enemyFields[ranValue].transform.position + offset;
        enemy.GetComponent<Enemy>().SetMainTarget(mainTargetCrystal);
    }

    internal void SetGameField(GameObject gameFields)
    {
        // TODO : 스테이지 별 에너미 필드 배열을 받아옴
        // 에너미 필드 배열을 받아옴 
        //GameObject gameField = Resources.Load<GameObject>("GameFields");
        for (int i = 0; i < gameFields.transform.childCount; i++)
        {
            GameObject enemyField = gameFields.transform.GetChild(i).gameObject;
            if (enemyField.name.Contains("EnemyField"))
            {
                enemyFields.Add(enemyField);
            }
        }
        hasGameField = true;
    }

    GameObject mainTargetCrystal;
    internal void SetCrystal(GameObject crystal)
    {
        mainTargetCrystal = crystal;
    }

    internal void SetNextStage(int nextStage)
    {
        // 이 함수가 불리면 GameStart가 된 시점
        currentStage = nextStage;
        respawnNormal = respawnNormal * (1 - (0.1f * (currentStage - 1)));
        respawnElite = respawnNormal * 5;
        respawnBoss = respawnElite * 5;
        // Int형으로 올림
        normalMaxCount = Mathf.CeilToInt((currentStage * 100) * 0.89f);
        eliteMaxCount = Mathf.CeilToInt((currentStage * 100) * 0.10f);
        bossMaxCount = Mathf.CeilToInt((currentStage * 100) * 0.01f);
        totalCount = normalMaxCount + eliteMaxCount + bossMaxCount;
        GameManager.Instance.TotalCount = totalCount;
    }
}
