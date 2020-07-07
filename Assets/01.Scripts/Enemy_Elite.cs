﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Elite : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attackers = new List<GameObject>();
        //DeathVFX = Resources.Load<GameObject>("SFX_WoodDeath");

        HP = 1000f;
        attackPower = 30f;
        attackSpeed = 3.5f;

        range = 3f;
        moveSpeed = 1.5f;

        dropGold = 50;

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
