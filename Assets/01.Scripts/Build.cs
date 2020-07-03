using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    // 모든 건물들의 부모클래스

    // 모든 건물들의 공통된 속성, 기능들

    // 체력, 건설비용, 판매비용, 다음업그레이드건물
    // 자신이 건설 된 게임 필드의 포지션
    // 업그레이드, 판매, 파괴

    protected int hp, buildCost, sellGold;
    public int HP { get { return hp; } set { hp = value; } }
    public int Cost { get { return buildCost; } set { buildCost = value; } }
    public int SellGold { get { return sellGold; } set { sellGold = value; } }

    protected GameObject nextUpgradeF;

    // 건물(타워)는 자신이 공격한 에너미가 죽을때 에너미에게서 죽었다고 신호를 받는다.
    // 



    // 타겟리스트
    // 타겟리스트의 0번째만 공격
    // 사정거리 안에 있는 몹 중 가장 먼저 들어온 놈 먼저 공격 
    // 얘네는 자식 클래스에서 각자 추가 / 삭제

    public List<GameObject> targetEnemies;

    public virtual void AddTarget(GameObject gameObject)
    {

    }
    public virtual void RemoveTarget(GameObject gameObject)
    {

    }
    public virtual void Sell()
    {
        // 자신의 sellGold만큼 골드를 추가하고 자신 파괴
        print("판매 비용은 : " + SellGold);
        GoldManager.Instance.Gold += SellGold;
        BuildDestroy(gameObject);
    }

    public virtual void BuildDestroy(GameObject obj)
    {
        // 파괴 애니메이션?
        buildedField.GetComponent<GameField>().isBuildable = true;
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


    // 건물이 건설된 필드의 정보를 가지고 있다
    protected GameObject buildedField;
    public void SetBuildedField(GameObject gameObject)
    {
        buildedField = gameObject;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        // 건물 생성시 크리스탈 반대방향을 바라보게 한다
        Vector3 dir = BuildManager.Instance.CrystalPosition - transform.position;
        dir.Normalize();
        transform.forward = dir;
        
    }

    // 공격한 에너미가 파괴되었을 때 받는 함수
    public virtual void SignalDeath(GameObject enemy)
    {
        // 타겟리스트에서 죽은 에너미 제외

        
    }

    // Update is called once per frame
    void Update()
    {

    }

}
