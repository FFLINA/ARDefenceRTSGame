using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class CanonTowerlvl4 : Tower
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
    public CanonTowerlvl4()
    {
        hp = 600f;
        maxHp = hp;
        buildCost = 400;
        sellGold = 525;     // (250 + 150 + 250 + 400) / 2
        attackSpeed = 3.5f;
        range = 25f;

    }
    public override void Awake()
    {
        base.Awake();

        nextUpgradeF = null;
        attackRangeShpereFactory = Resources.Load<GameObject>("AttackRange");
        //bulletFactory = Resources.Load<GameObject>("CanonBall_Lvl4");
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
        //ScaleManager.Instance.ScaleFixForAR(attackRangeShpere);

        hpSlider.maxValue = hp;
        hpSlider.value = hp;
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
