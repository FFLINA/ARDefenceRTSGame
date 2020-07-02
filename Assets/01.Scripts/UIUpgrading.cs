using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgrading : MonoBehaviour
{
    // 건물을 눌렀을때 나오는 기능들의 UI 스크립트


    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickUpgrade()
    {
        target.GetComponent<Build>().Upgrade();
        Destroy(gameObject);
    }

    public void OnClickSell()
    {
        target.GetComponent<Build>().Sell();
        Destroy(gameObject);
    }

    public void OnClickCancel()
    {
        Destroy(gameObject);
    }
}
