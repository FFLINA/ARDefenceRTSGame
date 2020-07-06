using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerlvl1 : Tower
{
    // TODO: 건설 시간동안 딜레이 추가
    public override float AttackSpeed
    { get { return attackSpeed; } set { attackSpeed = value; } }
    public override float AttackRange
    { get { return range; } set { range = value; } }
    public override void AddTarget(GameObject gameObject)
    {
        targetEnemies.Add(gameObject);
    }
    public override void RemoveTarget(GameObject gameObject)
    {
        targetEnemies.Remove(gameObject);
    }

    public override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();

        // 이 타워의 타겟리스트 생성
        targetEnemies = new List<GameObject>();

        hp = 100f;
        buildCost = 50;
        sellGold = buildCost / 2;
        attackSpeed = 1f;
        range = 10f;

        // 생성 될 때 자신의 비용만큼 골드 차감
        GoldManager.Instance.Gold -= Cost;

        state = State.SEARCH;
        nextUpgradeF = Resources.Load<GameObject>("ArcherTower_Lvl2");

        // 반지름이 AttackRange인 구체
        attackRangeShpereFactory = Resources.Load<GameObject>("AttackRange");
        attackRangeShpere = Instantiate(attackRangeShpereFactory);
        attackRangeShpere.transform.parent = transform;
        attackRangeShpere.transform.localScale =
            new Vector3(AttackRange * 2, AttackRange * 2, AttackRange * 2);
        attackRangeShpere.transform.position = transform.position;

        bulletFactory = Resources.Load<GameObject>("Arrow");
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    public override void Sell()
    {
        base.Sell();
    }

    public override void BuildDestroy()
    {
        base.BuildDestroy();
    }
    public override void Upgrade()
    {
        base.Upgrade();
    }
}
