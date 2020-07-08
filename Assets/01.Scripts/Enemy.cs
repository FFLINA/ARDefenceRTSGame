﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class Enemy : MonoBehaviour
{
    //  적

    // 적은 타워에 사거리에 들어갈 때 공격자(타워) 리스트를 저장하고
    // 죽을 때 공격자 리스트 전원에게 죽었다고 알림

    // 체력, 공격력, 사정거리, 공격속도, 메인타겟, 근처타겟, 처치시골드
    // 이동, 탐색, 공격, 파괴, 

    // TODO : 상태머신 인터페이스로 구현 
    public enum State
    {
        IDLE, MOVE, ATTACK
    }

    protected EffectClipsEnum attackEffectClips;
    protected EffectClipsEnum deathEffectClips;
    public virtual void OnAttackEventCall()
    {
        attackEffect = Instantiate(attackVFX);
        attackEffect.transform.position = attackPoint.position;
        Destroy(attackEffect, 1f);
        AttackTarget(currentTarget);

        SoundManager.Instance.PlayEffect(attackEffectClips, 0.25f);
    }

    internal void OnAttackEndEventCall()
    {

        anim.SetTrigger("Idle");
    }

    public State state;
    //protected State state;

    protected List<GameObject> attackers;

    // 메인 타겟은 MainCrystal
    protected GameObject crystalTarget;
    protected GameObject nearTarget;
    protected GameObject currentTarget;

    internal void SetNearTarget(GameObject nearTower)
    {
        nearTarget = nearTower;
    }

    float hp;
    public float HP
    {
        get { return hp; }
        set
        {
            hp = value;
            // TODO : HP UI 변경
            if (hp <= 0)
            {
                // 죽음

                EnemyDestroy();
            }
        }
    }

    public float AttackPower
    { get { return attackPower; } set { attackPower = value; } }
    public float AttackSpeed
    { get { return attackSpeed; } set { attackSpeed = value; } }
    public float AttackRange
    { get { return range; } set { range = value; } }
    public float MoveSpeed
    { get { return moveSpeed; } set { moveSpeed = value; } }

    protected float attackPower;
    protected float attackSpeed;
    protected float range;
    protected float moveSpeed;

    protected float tempTime;

    protected int dropGold;

    protected GameObject attackRangeShpereFactory;
    protected GameObject attackRangeShpere;

    protected GameObject attackVFX;
    GameObject attackEffect;
    Transform attackPoint;

    protected GameObject DeathVFX;
    public Animator anim;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        state = State.IDLE;

        nearTarget = null;
        currentTarget = crystalTarget;
        DeathVFX = Resources.Load<GameObject>("VFX_EnemyDeath");

        attackPoint = transform.Find("AttackPoint").transform;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        switch (state)
        {
            case State.IDLE:
                IdleUpdate();
                break;

            case State.MOVE:
                MoveUpdate();
                break;

            case State.ATTACK:
                AttackUpdate();
                break;

            default:
                break;
        }
    }

    protected virtual void IdleUpdate()
    {
        if (currentTarget != null)
        {
            state = State.MOVE;
            anim.SetTrigger("Move");
        }
        // 모든 타겟이 null이면 게임 끝, 정지

    }

    protected virtual void MoveUpdate()
    {
        // 메인 타겟을 향해 움직이다가
        if (currentTarget != null)
        {
            Vector3 dir = currentTarget.transform.position - transform.position;
            dir.Normalize();

            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.forward = dir;

            // 사정거리 안에 건물(or유닛) 이 들어오면 그 타겟을 공격
            if (nearTarget != null)
            {
                currentTarget = nearTarget;
                dir = currentTarget.transform.position - transform.position;
                dir.Normalize();
                transform.forward = dir;
                state = State.ATTACK;
                tempTime = AttackSpeed;

            }
        }
    }

    protected virtual void AttackUpdate()
    {
        tempTime += Time.deltaTime;

        if (currentTarget == null)
        {
            if (crystalTarget != null)
            {
                currentTarget = crystalTarget;
            }   // 크리스탈이 없으면 게임 끝
            state = State.IDLE;
            anim.SetTrigger("Idle");
        }

        // 해당 타겟을 공격
        // 공격할 수 있는 시간이 되면
        if (AttackSpeed <= tempTime)
        {
            // 공격 애니메이션 시작
            //AttackTarget(currentTarget);
            anim.SetTrigger("Attack");
            tempTime = 0;
        }
    }

    protected virtual void AttackTarget(GameObject currentTarget)
    {
        if (currentTarget.CompareTag("Crystal"))
        {
            // 크리스탈 공격
            Crystal.Instance.HP -= AttackPower;
            UIManager.Instance.SetMessageUI("Crystal is UnderAttack");
        }
        else if (currentTarget.CompareTag("Building"))
        {
            // 건물 공격
            print("Building Attack.");
            currentTarget.transform.parent.GetComponent<Build>().HP -= AttackPower;
        }
    }

    internal void SetMainTarget(GameObject crystal)
    {
        crystalTarget = crystal;
    }

    internal void HitBullet(int damage)
    {
        //TODO :: 이펙트 표시, Enemy UI표시
        HP -= damage;
    }

    protected virtual void EnemyDestroy()
    {
        // TODO : 사망 모션, 사망이펙트, 골드, UI 등
        GoldManager.Instance.Gold += dropGold;
        if (attackers.Count != 0)
        {
            // 죽을때 공격자한테 나 죽었다고 알린다
            for (int i = 0; i < attackers.Count; i++)
            {
                if (attackers[i] != null)
                {
                    attackers[i].GetComponent<Tower>().EnemyDeathSignal(gameObject);
                }
            }
        }
        attackers.Clear();
        // 에너미 파괴됨
        Destroy(gameObject);
        GameObject vfx = Instantiate(DeathVFX);
        vfx.transform.position = transform.position;
        vfx.SetActive(true);

        SoundManager.Instance.PlayEffect(deathEffectClips, 0.5f);

        anim.SetTrigger("Die");
    }

    internal void SetAttacker(GameObject tower)
    {
        // 에너미는 누구한테 공격당했는지 정보를 받는다
        // 공격당했는지가 아니라 누구 범위안에 들어왔는지를 받는다
        attackers.Add(tower);
    }

    internal void RemoveAttacker(GameObject tower)
    {
        attackers.Remove(tower);
    }
}
