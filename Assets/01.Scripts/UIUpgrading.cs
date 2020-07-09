using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgrading : MonoBehaviour
{
    // 건물을 눌렀을때 나오는 기능들의 UI 스크립트


    GameObject target;
    float dist;
    // Start is called before the first frame update
    void Start()
    {
        dist = Vector3.Distance(transform.position, Camera.main.transform.position);
        transform.localScale = transform.localScale.normalized * (dist / 3) * 7f;

        target = transform.parent.gameObject;
        int cost = target.GetComponent<Build>().GetNextCost();
        if (cost == 0)
        {
            upgradeText.text = upgradeMent + "\nMax";
        }
        else
        {
            upgradeText.text = upgradeMent + "\n$ " + cost.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.localScale.normalized * (dist / 3) * 7f;
    }

    public Text upgradeText;
    string upgradeMent = "Upgrade";

    public void OnClickUpgrade()
    {
        target.GetComponent<Build>().Upgrade();
        Destroy(gameObject);
    }

    public void OnClickSell()
    {
        target.GetComponent<Build>().Sell();
        int sellGold = target.GetComponent<Build>().SellGold;
        Destroy(gameObject);
    }

    public void OnClickCancel()
    {
        Destroy(gameObject);
    }
}
