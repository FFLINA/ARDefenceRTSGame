using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class Enemy_Boss : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attackers = new List<GameObject>();
        //DeathVFX = Resources.Load<GameObject>("SFX_GolemDeath");

        HP = 3000f;
        attackPower = 80f;
        attackSpeed = 4f;

        range = 4f;
        moveSpeed = 2.5f;
        moveSpeed = ScaleManager.Instance.MoveSpeedFixForAR(moveSpeed);


        dropGold = 500;

        attackEffectClips = EffectClipsEnum.EnemyGolemAttack;
        deathEffectClips = EffectClipsEnum.EnemyGolemDeath;
        // 반지름이 AttackRange인 구체
        attackRangeShpereFactory = Resources.Load<GameObject>("EnemyAttackRange");
        attackRangeShpere = Instantiate(attackRangeShpereFactory);
        attackRangeShpere.transform.parent = transform;
        attackRangeShpere.transform.localScale =
            new Vector3(AttackRange * 2, AttackRange * 2, AttackRange * 2);
        attackRangeShpere.transform.position = transform.position;
        //ScaleManager.Instance.ScaleFixForAR(attackRangeShpere);

        attackVFX = Resources.Load<GameObject>("VFX_GolemAttack");
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

        //EnemyManager.Instance.TotalCount--;
        EnemyManager.Instance.BossDestroySignal();
    }

}
