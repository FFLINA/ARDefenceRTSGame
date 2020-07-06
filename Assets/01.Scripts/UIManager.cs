using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

// TODO : 게임필드 생성은 GameManager로 이동
// 땅을 클릭하면 (해당스테이지의) 게임필드와 크리스탈이 생성되고 게임이 시작된다
// 게임필드를 클릭하면 UIBuild 가 생성, 그자리에 표시
// 건설된 건물을 클릭하면 UIUpgrade가 생성, 그자리(건물의 Point)에 표시
// 한번에 하나의 UI만 표시, 중복 방지 필요
// UIUpgrade 표시 시 
public class UIManager : MonoBehaviour
{
    public GameObject buildUIFactory;
    public GameObject upgradeUIFactory;
    GameObject buildUI, upgradeUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.hasCrystal == true)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mos = Input.mousePosition;
                mos.z = Camera.main.farClipPlane;
                Vector3 dir = Camera.main.ScreenToWorldPoint(mos);
                RaycastHit hit;
                int layerMask = 1 << 10 | 1 << 11;
                // 레이어 10 = AttackRange | 11 = Enemy
                if (Physics.Raycast(Camera.main.transform.position, dir, out hit, mos.z, ~layerMask))   // 레이어10 제외
                {
                    if (hit.transform.CompareTag("GameField"))
                    {
                        CreateUIBuild(hit);
                    }
                    else if (hit.transform.CompareTag("Building"))
                    {
                        CreateUIUpgrade(hit);
                    }
                    else if (hit.transform.CompareTag("Crystal"))
                    {
                        // TODO : 크리스탈의 상세정보를 표시
                    }
                }
            }
        }
    }

    

    private void CreateUIBuild(RaycastHit hit)
    {
        if(buildUI == null && upgradeUI == null)
        {
            // 해당 필드에 건물이 없 을 때만
            if (hit.transform.GetComponent<GameField>().isBuildable == true)
            {
                buildUI = Instantiate(buildUIFactory);
                // 게임필드 클릭 , 클릭된 게임필드 위치에 ui 표시, 클릭된 게임필드 위치에 건설,
                Vector3 offset = new Vector3(0, hit.transform.localScale.y / 2, 0);
                buildUI.transform.position = hit.transform.position + offset;


                buildUI.GetComponent<UIBuilding>().SetClickedField(hit.transform.gameObject);
                /* 해당 게임필드의 정보를 건물이 가지고 있다가
                * 건물이 파괴,판매 되면 가지고있던 게임필드의 isBuildable을 true로
                */
            }
        }
        
    }
    private void CreateUIUpgrade(RaycastHit hit)
    {
        if (buildUI != null)
        {
            Destroy(buildUI);
        }
        // 해당 건물의 UI를 보여준다
        if (upgradeUI == null)
        {
            upgradeUI = Instantiate(upgradeUIFactory);
            // UI를 타워의 자식으로
            upgradeUI.transform.parent = hit.transform.parent; // hit은 모델링이라

            // 임시
            upgradeUI.transform.position = upgradeUI.transform.parent.Find("Point").transform.position;
            if (buildUI != null)
            {
                Destroy(buildUI);
            }
        }
    }
}

/* 카메라 화면에서 화면 터치 시 그 부분에 레이를 쏘는 코드
 * Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {

        }
*/
