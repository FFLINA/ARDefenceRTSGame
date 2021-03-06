﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Normal : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attackers = new List<GameObject>();

        HP = 100f;
        attackPower = 5f;
        attackSpeed = 1f;
        range = 2f;
        moveSpeed = 5.0f;

        dropGold = 20;

        // 반지름이 AttackRange인 구체
        attackRangeShpereFactory = Resources.Load<GameObject>("EnemyAttackRange");
        attackRangeShpere = Instantiate(attackRangeShpereFactory);
        attackRangeShpere.transform.parent = transform;
        attackRangeShpere.transform.localScale =
            new Vector3(AttackRange * 2, AttackRange * 2, AttackRange * 2);
        attackRangeShpere.transform.position = transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void IdleUpdate()
    {
        base.IdleUpdate();
    }
    protected override void MoveUpdate()
    {
        base.MoveUpdate();
    }

    protected override void AttackUpdate()
    {
        base.AttackUpdate();
    }
    protected override void AttackTarget(GameObject currentTarget)
    {
        base.AttackTarget(currentTarget);
    }
    protected override void EnemyDestroy()
    {
        base.EnemyDestroy();
    }
}
