using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerlvl3 : Build
{
    // 아처타워 레벨3 스크립트


    // TODO: 건설 시간동안 딜레이 추가
    // TODO: 건설 비용 만큼 골드 감소
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
        hp = 500;
        buildCost = 150;
        sellGold = 30;

        // 생성 될 때 자신의 비용만큼 골드 차감
        print("이 건물의 비용은 : " + Cost);
        GoldManager.Instance.Gold -= Cost;

        state = State.SEARCH;
        nextUpgradeF = null;
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

    public override void BuildDestroy(GameObject obj)
    {
        base.BuildDestroy(obj);
    }


    public override void Upgrade()
    {
        base.Upgrade();
    }
}
