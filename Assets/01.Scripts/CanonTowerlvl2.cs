using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class CanonTowerlvl2 : Tower
{
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
    public CanonTowerlvl2()
    {
        // 속성 설정
        hp = 250f;
        buildCost = 150;
        sellGold = 200;     // 250 + 150 / 2
        attackSpeed = 5.5f;
        range = 15f;
    }
    public override void Awake()
    {
        base.Awake();

        nextUpgradeF = Resources.Load<GameObject>("CanonTowerLvl3");
        attackRangeShpereFactory = Resources.Load<GameObject>("AttackRange");
        bulletFactory = Resources.Load<GameObject>("CanonBall_Lvl2");
        attackEffectClip = EffectClipsEnum.CanonFire;
    }
    public override void Start()
    {
        base.Start();

        // 이 타워의 타겟리스트 생성
        targetEnemies = new List<GameObject>();


        // 생성 될 때 자신의 비용만큼 골드 차감
        GoldManager.Instance.Gold -= Cost;

        state = State.SEARCH;

        // 반지름이 AttackRange인 구체
        attackRangeShpere = Instantiate(attackRangeShpereFactory);
        attackRangeShpere.transform.parent = transform;
        attackRangeShpere.transform.localScale =
            new Vector3(AttackRange * 2, AttackRange * 2, AttackRange * 2);
        attackRangeShpere.transform.position = transform.position;

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
