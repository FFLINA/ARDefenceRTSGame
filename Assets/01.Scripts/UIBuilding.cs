using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuilding : MonoBehaviour
{
    // 건물을 건설하는 UI 스크립트


    GameObject archerTowerFactory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickArcherTower()
    {
        print("아처 타워 건설 클릭");
        // 예정: 중요도 하 - 건설 시간동안 딜레이 추가
        // 예정: 중요도 상 - 건설 비용 만큼 골드 감소
        
        archerTowerFactory = Resources.Load<GameObject>("ArcherTower_Lvl1");
        GameObject archerTower = Instantiate(archerTowerFactory);
        archerTower.transform.position = transform.position;
        Destroy(gameObject);
    }
    
    public void OnClickCancel()
    {
        Destroy(gameObject);
    }
}
