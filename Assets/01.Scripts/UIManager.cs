using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 게임 화면에서 팝업되는 UI를 담당하는 매니저 스크립트


    public GameObject buildUIFactory;
    public GameObject upgradeUIFactory;
    GameObject buildUI, upgradeUI;

    GameObject gameFieldFactory;
    GameObject gameField;

    GameObject crystalFactory;
    GameObject crystal;
    // Start is called before the first frame update
    void Start()
    {
        crystalFactory = Resources.Load<GameObject>("MainCrystal");
        gameFieldFactory = Resources.Load<GameObject>("GameField");
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
                    if(gameField == null)
                    {
                        // 게임필드를 만들고
                        print("게임필드 설치 ");
                        gameField = Instantiate(gameFieldFactory);
                        gameField.transform.position = hit.point;
                        print("크리스탈 설치 ");
                        crystal = Instantiate(crystalFactory);
                        crystal.transform.position = hit.point;
                        // 크리스탈 포지션 저장
                        BuildManager.Instance.CrystalPosition = crystal.transform.position;
                    }
                }
                // 닿은 곳의 태그가 게임필드면
                else if (hit.transform.CompareTag("GameField"))
                {
                    // 근데 크리스탈이 있을때만
                    if (crystal != null)
                    {
                        if (buildUI == null && upgradeUI == null)
                        {
                            buildUI = Instantiate(buildUIFactory);
                            buildUI.transform.position = hit.point;
                        }
                    }
                }
                else if (hit.transform.CompareTag("Building"))
                {
                    if (crystal != null)
                    {
                        // 해당 건물의 UI를 보여준다
                        print("Clicked Building");
                        if (upgradeUI == null && buildUI == null)
                        {
                            upgradeUI = Instantiate(upgradeUIFactory);
                            upgradeUI.transform.parent = hit.transform;
                            // 임시
                            upgradeUI.transform.position = hit.transform.GetChild(0).transform.position;
                        }
                    }
                }
                else if (hit.transform.CompareTag("Crystal"))
                {
                    print("크리스탈 클릭됨");
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
