using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerlvl1 : Build
{
    // 아처타워 레벨1 스크립트


    // TODO: 건설 시간동안 딜레이 추가
    // TODO: 건설 비용 만큼 골드 감소
    // 탐색, 공격
    enum State
    {
        SEARCH, ATTACK
    }
    State state;

    // 타워는 여러 총알중 한 종류를 발사만 하고 공격력을 가지고있지않다
    // 공격력은 총알이 가지고 있다

    // 타워는 에너미에게 총알을 발사 할 때 에너미에게 내가 쐈다고 나의 정보를 전달한다

    // 공격속도, 사정거리
    float attackSpeed, range;

    public float AttackSpeed
    { get { return attackSpeed; } set { attackSpeed = value; } }
    public float AttackRange
    { get { return range; } set { range = value; } }
    GameObject attackRangeShpereFactory;
    GameObject attackRangeShpere;

    GameObject bulletFactory;
    GameObject bullet;

    //List<GameObject> targetEnemies;

    public override void Start()
    {
        base.Start();
        // 이 타워의 타겟리스트 생성
        targetEnemies = new List<GameObject>();

        hp = 100;
        buildCost = 50;
        sellGold = 10;
        AttackSpeed = 1f;
        AttackRange = 10f;

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

        bulletFactory = Resources.Load<GameObject>("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
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

    float tempTime = 0;

    GameObject target;

    void AttackUpdate()
    {
        if (targetEnemies.Count != 0)
        {
            if (AttackSpeed <= tempTime)
            {
                // TODO : 총알 종류 설정
                bullet = Instantiate(bulletFactory);
                bullet.transform.position = transform.position;
                //for (int i = 0; i < targetEnemies.Count; i++)
                //{
                //    if(targetEnemies[i] != null)
                //    {
                //        target = targetEnemies[i];
                //        break;
                //    }
                //}
                bullet.GetComponent<Bullet>().SetTarget(targetEnemies[0].transform.Find("Model").gameObject);
                // 발사한 적 한테 내가 공격했다고 전달
                // 발사할 때 가 아니라 레인지안에 들어갓을 때 정보 전달
                
                tempTime = 0;
            }
        }
        else
        {
            state = State.SEARCH;
        }


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

    public override void AddTarget(GameObject gameObject)
    {
        targetEnemies.Add(gameObject);
    }
    public override void RemoveTarget(GameObject gameObject)
    {
        targetEnemies.Remove(gameObject);
    }
    public override void SignalDeath(GameObject enemy)
    {
        // 타겟리스트에서 죽은 에너미 제외
        RemoveTarget(enemy);
    }
}
