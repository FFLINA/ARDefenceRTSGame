using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    private void Awake()
    {
        Instance = this;
        hasGameField = false;
    }

    // 게임필드가 생성됐을 때
    bool hasGameField;
    // 에너미 필드 배열을 받아옴
    List<GameObject> enemyFields = new List<GameObject>();

    // 임시
    // 2초마다 에너미 생성
    // 프리팹 공장
    GameObject enemyF;
    // 생성시간
    float respawnTime = 2f;
    float tempTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyF = Resources.Load<GameObject>("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if(hasGameField == true)
        {
            tempTime += Time.deltaTime;
            if (tempTime >= respawnTime)
            {
                // 적 생산
                GameObject enemy = Instantiate(enemyF);
                Vector3 offset = new Vector3(0, enemyFields[0].transform.localScale.y, 0) /2;
                int ranValue = UnityEngine.Random.Range(0, enemyFields.Count);
                enemy.transform.position = enemyFields[ranValue].transform.position + offset;
                enemy.GetComponent<Enemy>().SetMainTarget(mainTargetCrystal);
                tempTime = 0;
            }
        }

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
}
