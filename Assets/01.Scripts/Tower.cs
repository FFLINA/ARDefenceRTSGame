using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static SoundManager;

public class Tower : Build
{
    // Build
    // 체력, 건설비용, 판매비용, 다음업그레이드건물
    // 자신이 건설 된 게임 필드의 포지션
    // 업그레이드, 판매, 파괴

    // TODO : 상태머신 인터페이스로 구현 
    // Tower
    // 상태 : 탐색, 공격, (설치중)
    // 공격속도, 사거리, 사거리충돌구체, 총알, 타겟리스트
    // 타겟추가, 타겟삭제, 처치 신호, 

    // 타워는 여러 총알중 한 종류를 발사만 하고 공격력을 가지고있지않다
    // 공격력은 총알이 가지고 있다

    // 타워는 자신이 공격한 에너미가 죽을때 에너미에게서 죽었다고 신호를 받는다.
    // 타워 자신이 죽을 때 타겟리스트의 모두에게 자신이 죽었다고 신호를받는다
    // -> 신호를 받은 타겟리스트(에너미)들은 attacker리스트에서 신호를준 타워를 제외한다

    // 타겟리스트
    // 타겟리스트의 0번째만 공격
    // 사정거리 안에 있는 몹 중 가장 먼저 들어온 놈 먼저 공격 
    // 얘네는 자식 클래스에서 각자 추가 / 삭제
    protected enum State
    {
        SEARCH, ATTACK
    }
    protected State state;

    protected float attackSpeed, range;

    public virtual float AttackSpeed    
    { get { return attackSpeed; } set { attackSpeed = value; } }
    public virtual float AttackRange
    { get { return range; } set { range = value; } }

    protected GameObject attackRangeShpereFactory;
    protected GameObject attackRangeShpere;

    // TODO : 총알 종류 설정
    protected GameObject bulletFactory;
    protected GameObject bullet;

    public List<GameObject> targetEnemies;

    public virtual void AddTarget(GameObject gameObject)
    {

    }
    public virtual void RemoveTarget(GameObject gameObject)
    {

    }

    // 공격한 에너미가 파괴되었을 때 받는 함수
    public void EnemyDeathSignal(GameObject enemy)
    {
        // 타겟리스트에서 죽은 에너미 제외
        RemoveTarget(enemy);
    }
    public override void Sell()
    {
        base.Sell();
        BuildDestroy();
    }

    // 팔거나 업그레이드할때 원래 지어진 게임필드의 정보를 잃어버림

    public override void BuildDestroy()
    {
        base.BuildDestroy();

        buildedField.GetComponent<GameField>().isBuildable = true;
        for(int i = 0; i < targetEnemies.Count; i++)
        {
            targetEnemies[i].GetComponent<Enemy>().RemoveAttacker(gameObject);
        }
        Destroy(gameObject);
    }

    public override void Upgrade()
    {
        base.Upgrade();
    }
    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
    }


    float tempTime = 0;

    public override void Update()
    {
        base.Update();

        // 공격속도를 위한 tempTime은 어떤상태여도 계속증가
        tempTime += Time.deltaTime;

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
        // 자신의 사정거리 안에 들어온 적이 있을 때
        if (targetEnemies.Count != 0)
        {
            state = State.ATTACK;
        }
    }

    void AttackUpdate()
    {
        CheckEmptyTarget();
        if (AttackSpeed <= tempTime)
        {
            bullet = Instantiate(bulletFactory);
            ScaleManager.Instance.ScaleFixForAR(bullet);
            bullet.transform.position = transform.Find("Point").position;
            if (targetEnemies.Count != 0)
            {
                bullet.GetComponent<Bullet>().SetTarget(targetEnemies[0].transform.Find("Model").gameObject);
            }

            // 공격 사운드 이펙트
            SoundManager.Instance.PlayEffect(attackEffectClip, 0.15f);

            tempTime = 0;
        }
    }
    private void CheckEmptyTarget()
    {
        if (targetEnemies.Count != 0)
        {
            if (targetEnemies[0].gameObject == null)
            {
                targetEnemies.RemoveAt(0);
            }
        }
        else
        {
            state = State.SEARCH;
        }
    }
}
