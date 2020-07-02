using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerlvl1 : Build
{
    // 아처타워 레벨1 스크립트


    // 예정: 건설 시간동안 딜레이 추가
    // 예정: 건설 비용 만큼 골드 감소
    // 탐색, 공격
    enum State
    {
        SEARCH, ATTACK
    }
    State state;

    // 공격력, 사정거리
    int t_Attack, t_range;

    public int AttackPoint
    { get { return t_Attack; } set { t_Attack = value; } }
    public int AttackRange
    { get { return t_range; } set { t_range = value; } }

    public override void Start()
    {
        base.Start();
        hp = 100;
        buildCost = 50;
        sellGold = 10;

        // 생성 될 때 자신의 비용만큼 골드 차감
        print("이 건물의 비용은 : " + Cost);
        GoldManager.Instance.Gold -= Cost;

        state = State.SEARCH;
        nextUpgradeF = Resources.Load<GameObject>("ArcherTower_Lvl2");
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.SEARCH:
                SearchUpdate();
                break;

            case State.ATTACK:
                AttackUpdate();
                break;

            default:
                break;
        }
    }

    void SearchUpdate()
    {
        
    }
    void AttackUpdate()
    {

    }

    public override void Sell()
    {
        base.Sell();
    }

    public override void Destroyed(GameObject obj)
    {
        base.Destroyed(obj);
    }

    public override void Upgrade()
    {
        base.Upgrade();
    }
}
