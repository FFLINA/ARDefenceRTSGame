using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuilding : MonoBehaviour
{
    // 건물을 건설하는 UI 스크립트


    GameObject archerTowerFactory;
    GameObject canonTowerFactory;
    float dist;
    // Start is called before the first frame update
    void Start()
    {
        dist = Vector3.Distance(transform.position, Camera.main.transform.position);

        transform.localScale = transform.localScale.normalized * (dist / 3);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.localScale = transform.localScale.normalized * (dist / 3);
    }


    public void OnClickArcherTower()
    {
        // TODO: 중요도 하 - 건설 시간동안 딜레이 추가

        // 해당 필드가 건설 가능일때만
        if (clickedField.GetComponent<GameField>().isBuildable == true)
        {
            archerTowerFactory = Resources.Load<GameObject>("ArcherTowerLvl1");
            GameObject archerTower = Instantiate(archerTowerFactory);
            ScaleManager.Instance.ScaleFixForAR(archerTower);

            if (GoldManager.Instance.Gold >= archerTower.GetComponent<Build>().Cost)
            {
                Vector3 offset = new Vector3(0, 0.1f, 0);
                archerTower.transform.position = transform.position + offset;
                archerTower.GetComponent<Build>().SetBuildedField(clickedField);
                // 건설된 필드의 isBuilded 를 false로
                clickedField.GetComponent<GameField>().isBuildable = false;
                Destroy(gameObject);
            }
            else
            {
                Destroy(archerTower);
                UIManager.Instance.SetMessageUI("Not Enough Money.");
            }

        }
        else
        {
            UIManager.Instance.SetMessageUI("Already Builded.");
        }
    }

    public void OnClickCanonTower()
    {
        if (clickedField.GetComponent<GameField>().isBuildable == true)
        {
            canonTowerFactory = Resources.Load<GameObject>("CanonTowerLvl1");
            GameObject canonTower = Instantiate(canonTowerFactory);
            ScaleManager.Instance.ScaleFixForAR(canonTower);

            if (GoldManager.Instance.Gold >= canonTower.GetComponent<Build>().Cost)
            {
                Vector3 offset = new Vector3(0, 0.1f, 0);
                canonTower.transform.position = transform.position + offset;
                canonTower.GetComponent<Build>().SetBuildedField(clickedField);
                // 건설된 필드의 isBuilded 를 false로
                clickedField.GetComponent<GameField>().isBuildable = false;
                Destroy(gameObject);
            }
            else
            {
                Destroy(canonTower);
                UIManager.Instance.SetMessageUI("Not Enough Money.");
            }
            
        }
        else
        {
            UIManager.Instance.SetMessageUI("Already Builded");
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
