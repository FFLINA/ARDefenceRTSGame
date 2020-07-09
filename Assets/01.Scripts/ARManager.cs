using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

public class ARManager : MonoBehaviour
{
    // 메인카메라가 보는 곳에 Indicator를 가져다 놓고 싶다
    public GameObject indicator;
    public GameObject ground;
    public GameObject monsterF;

    ARRaycastManager arRaycastManager;
    Vector2 center;
    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        center = new Vector2(Screen.width / 2, Screen.height / 2);

        // project settings -> Symbol에 추가
#if !UNITY_EDITOR
        // 실행한 기기가 유니티가 아니라면
        // Ground를 보이지 않게 하고싶다
        ground.SetActive(false);
#endif
        



    }


    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        UpdateUnity();
#else
        UpdateARCamera();
#endif
        UpdateMonsTouch();
    }

    private void UpdateMonsTouch()
    {
        // Indicator가 보일때 화면 터치로 Indicator를 선택하면 그 자리에 몬스터를 배치
        // 인디케이터가 활성화 되었을 때
        if (indicator.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    // 만약 그 물체가 Indicator면 그 자리에 몬스터 공장에서 몬스터를 생성, 위치
                    //if(hitInfo.transform.gameObject.name.Contains("Indicator"))
                    if (hitInfo.transform.gameObject == indicator)
                    {
                        //GameObject mon = Instantiate(monsterF);
                        //mon.transform.position = hitInfo.point;
                        GameManager.Instance.CreateGameField(hitInfo);
                        Camera.main.fieldOfView = 90f;
                        
                        indicator.SetActive(false);
                        isGamePlaying = true;
                    }
                }
            }
        }

    }
    bool isGamePlaying = false;
    private void UpdateARCamera()
    {
        if(isGamePlaying == false)
        {
            // 부딪힌 정보를 가지고 오고싶다.
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (arRaycastManager.Raycast(center, hits))
            {
                // 그 정보에서 부딪힌 위치를 가져와서
                // Indicator를 그곳에가져다 놓고싶다
                indicator.SetActive(true);
                indicator.transform.position = hits[0].pose.position;
            }
            else
            {
                indicator.SetActive(false);
            }
        }
    }

    private void UpdateUnity()
    {
        // 시선을 만들고
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        // 부딪힌 정보 변수를 만들고
        RaycastHit hit;
        // 시선을 이용해서 바라보고싶다
        // 바라본 곳의 정보가 존재한다면
        int layer = 1 << LayerMask.NameToLayer("Indicator");
        if (Physics.Raycast(ray, out hit, 10000, ~layer))
        {
            // 인디케이터를 보이게하고 부딪힌곳에 이동
            indicator.SetActive(true);
            indicator.transform.position = hit.point;
        }
        else
        {
            indicator.SetActive(false);
        }
    }
}
