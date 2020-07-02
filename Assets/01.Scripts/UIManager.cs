using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 게임 화면에서 팝업되는 UI를 담당하는 매니저 스크립트


    public GameObject buildUIFactory;
    public GameObject upgradeUIFactory;
    GameObject buildUI, upgradeUI;

    GameObject crystalFactory;
    GameObject crystal;
    // Start is called before the first frame update
    void Start()
    {
        crystalFactory = Resources.Load<GameObject>("MainCrystal");
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

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, dir, out hit, mos.z))
            {
                if (hit.transform.CompareTag("GameField"))
                {
                    // 크리스탈이 없으면
                    if (crystal == null)
                    {
                        // 크리스탈을 생성하고
                        print("크리스탈 설치 ");
                        crystal = Instantiate(crystalFactory);
                        crystal.transform.position = hit.point;
                    }
                    // 크리스탈이 있으면
                    else
                    {
                        // 건설UI를 보여준다
                        if (buildUI == null)
                        {
                            if (upgradeUI != null)
                            {
                                Destroy(upgradeUI);
                            }
                            buildUI = Instantiate(buildUIFactory);
                            buildUI.transform.position = hit.point;
                            buildUI.SetActive(true);
                        }
                    }
                }
                else if (hit.transform.CompareTag("Building"))
                {
                    if (crystal != null)
                    {
                        // 해당 건물의 UI를 보여준다
                        print("Clicked Building");
                        if (upgradeUI == null)
                        {
                            if (buildUI != null)
                            {
                                Destroy(buildUI);
                            }
                            upgradeUI = Instantiate(upgradeUIFactory);
                            upgradeUI.transform.parent = hit.transform;
                            upgradeUI.SetActive(true);
                            // 임시
                            upgradeUI.transform.position = hit.transform.GetChild(0).transform.position;
                        }
                    }
                }
            }
        }
    }
}
