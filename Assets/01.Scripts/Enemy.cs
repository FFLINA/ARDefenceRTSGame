using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 적
    // 적은 공격받을 때 누구한테 맞았는지 정보를 받고 (여려명일수 있으니 리스트로)
    // 죽을 때 공격자한테 죽었다고 신호를 보낸다 (리스트 전원에게 다)

    // ㄴ> 공격받았을 때 목록을 받는게 아니라 
    //     레인지 안에 들어갔을 때 누구 안에 들어갔는지를 받아야 함

    // 체력, 공격력, 사정거리, 공격속도, 메인타겟, 근처타겟, 처치시골드

    List<GameObject> attackers = new List<GameObject>();

    // 메인 타겟은 MainCrystal
    GameObject mainTarget;
    GameObject nearTarget;

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
                Destroyed();
            }
        }
    }

    float attackPower;
    float attackRange;
    float moveSpeed;
    int dropGold;



    // Start is called before the first frame update
    void Start()
    {
        HP = 3f;
        attackPower = 1f;
        attackRange = 3f;
        moveSpeed = 10.0f;
        dropGold = 20;
    }

    // Update is called once per frame
    void Update()
    {
        // 메인 타겟을 향해 움직이다가
        if (mainTarget != null)
        {
            Vector3 dir = mainTarget.transform.position - transform.position;
            dir.Normalize();

            transform.position += dir * moveSpeed * Time.deltaTime;

            // 적 상태머신
            // 탐색거리? 안에 건물(or유닛) 이 들어오면 타겟을 변경
            // 해당 타겟을 공격하러 이동, 사정거리 안에오면 공격
            // 해당 타겟이 파괴되면 다시 탐색
            // 탐색거리 안에 건물이없으면 메인 타겟인 MainCrystal 을 향해 이동
            // 있으면 그 건물을 타겟으로 변경 - > 반복
        }
    }

    internal void SetMainTarget(GameObject crystal)
    {
        mainTarget = crystal;
    }

    internal void HitBullet(int damage)
    {
        //TODO :: 이펙트 표시, Enemy UI표시
        HP -= damage;
    }
    private void Destroyed()
    {
        // 에너미 파괴됨
        Destroy(gameObject);
        // TODO : 사망 모션, 사망이펙트, 골드, UI 등
        GoldManager.Instance.Gold += dropGold;
        if (attackers.Count != 0)
        {
            // 죽을때 공격자한테 나 죽었다고 알린다
            for (int i = 0; i < attackers.Count; i++)
            {
                attackers[i].GetComponent<Build>().SignalDeath(gameObject);
            }
        }
    }

    internal void SetAttacker(GameObject attackableTower)
    {
        // 에너미는 누구한테 공격당했는지 정보를 받는다
        // 공격당했는지가 아니라 누구 범위안에 들어왔는지를 받는다
        attackers.Add(attackableTower);
    }
}
