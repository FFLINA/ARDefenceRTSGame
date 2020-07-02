using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    // 모든 건물들의 부모클래스

    // 모든 건물들의 공통된 속성, 기능들

    // 체력, 건설비용, 판매비용, 다음업그레이드건물
    // 업그레이드, 판매, 파괴

    protected int hp, buildCost, sellGold;
    public int HP { get { return hp; } set { hp = value; } }
    public int Cost { get { return buildCost; } set { buildCost = value; } }
    public int SellGold { get { return sellGold; } set { sellGold = value; } }

    protected GameObject nextUpgradeF;

    public virtual void Sell()
    {
        // 자신의 sellGold만큼 골드를 추가하고 자신 파괴
        print("판매 비용은 : " + SellGold);
        GoldManager.Instance.Gold += SellGold;
        Destroyed(gameObject);
    }

    public virtual void Destroyed(GameObject obj)
    {
        // 파괴 애니메이션?
        Destroy(obj);
    }

    public virtual void Upgrade()
    {
        if (nextUpgradeF != null)
        {
            GameObject nextTower = Instantiate(nextUpgradeF);
            nextTower.transform.position = transform.position;
            Destroy(gameObject);
        }
        else
        {
            print("다음 업그레이드 건물이 없습니다");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
