using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuilding : MonoBehaviour
{
    // 건물을 건설하는 UI 스크립트


    GameObject archerTowerFactory;
    GameObject canonTowerFactory;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, Camera.main.transform.position);
        transform.localScale = transform.localScale.normalized * (dist / 4);
    }
    public void OnClickArcherTower()
    {
        // TODO: 중요도 하 - 건설 시간동안 딜레이 추가

        // 해당 필드가 건설 가능일때만
        if (clickedField.GetComponent<GameField>().isBuildable == true)
        {
            archerTowerFactory = Resources.Load<GameObject>("ArcherTower_Lvl1");
            GameObject archerTower = Instantiate(archerTowerFactory);
            archerTower.transform.position = transform.position;
            archerTower.GetComponent<Build>().SetBuildedField(clickedField);
            // 건설된 필드의 isBuilded 를 false로
            clickedField.GetComponent<GameField>().isBuildable = false;
            Destroy(gameObject);
        }
        else
        {
            print("이미 건물이 건설되어 있습니다");
        }
    }

    public void OnClickCanonTower()
    {
        if (clickedField.GetComponent<GameField>().isBuildable == true)
        {
            canonTowerFactory = Resources.Load<GameObject>("CanonTower_Lvl1");
            GameObject canonTower = Instantiate(canonTowerFactory);
            canonTower.transform.position = transform.position;
            canonTower.GetComponent<Build>().SetBuildedField(clickedField);
            // 건설된 필드의 isBuilded 를 false로
            clickedField.GetComponent<GameField>().isBuildable = false;
            Destroy(gameObject);
        }
        else
        {
            print("이미 건물이 건설되어 있습니다");
        }
    }
    public void OnClickCancel()
    {
        Destroy(gameObject);
    }

    GameObject clickedField;
    internal void SetClickedField(GameObject gameObject)
    {
        clickedField = gameObject;
    }
}
