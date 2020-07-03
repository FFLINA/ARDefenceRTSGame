using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 게임 화면에서 팝업되는 UI를 담당하는 매니저 스크립트


    public GameObject buildUIFactory;
    public GameObject upgradeUIFactory;
    GameObject buildUI, upgradeUI;

    GameObject gameFieldsFactory;
    GameObject gameFields;

    GameObject crystalFactory;
    GameObject crystal;
    // Start is called before the first frame update
    void Start()
    {
        crystalFactory = Resources.Load<GameObject>("MainCrystal");
        gameFieldsFactory = Resources.Load<GameObject>("GameFields");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))  // 마우스가 클릭 되면
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
                // 닿은 곳의 태그가 게임필드면
                else if (hit.transform.CompareTag("GameField"))
                {
                    // UI 중복 방지
                    if (buildUI == null && upgradeUI == null)
                    {
                        // 해당 필드에 건물이 없을 때만
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
                else if (hit.transform.CompareTag("Building"))
                {
                    // 해당 건물의 UI를 보여준다
                    print("Clicked Building");
                    if (upgradeUI == null && buildUI == null)
                    {
                        upgradeUI = Instantiate(upgradeUIFactory);
                        // UI를 타워의 자식으로
                        upgradeUI.transform.parent = hit.transform.parent; // hit은 모델링이라

                        // 임시
                        upgradeUI.transform.position = upgradeUI.transform.parent.Find("UIPoint").transform.position;
                        
                    }

                }
                else if (hit.transform.CompareTag("Crystal"))
                {
                    // TODO : 크리스탈의 상세정보를 표시
                }
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
