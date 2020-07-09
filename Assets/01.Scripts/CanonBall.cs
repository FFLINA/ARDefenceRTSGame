using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : Bullet
{
    // TODO : 최우선으로 구현

    // 모델러가 있는 자식은 Y축으로 일정 높이까지 올렸다 목표의 Y까지 내리면서
    // 자신은 목표를 향해 X,Z축만 이동 -> 포물선 이동

    // 캐논은 발사 된 순간의 타겟 위치만 기억하고
    // 타겟이 죽더라도 타겟위치를 향해 계속해서 비행


    // y 축 이동을 담당하는 오브젝트
    GameObject child;

    // 올라갈 높이
    float parabolaHeight;

    // 최종 목적지
    Vector3 parabolaDestination;
    public Vector3 GetTargetPosition()
    {
        return parabolaDestination;
    }
    // 이동까지 걸리는 시간
    float moveTime;

    protected override void Start()
    {
        Destroy(gameObject, 5f);
        //child = transform.Find("HitCheckBall").gameObject;
        child = transform.GetChild(0).gameObject;
        AttackPower = 250;
        MoveSpeed = 50f;
        parabolaHeight = 3f;
        moveTime = 1.5f;
        Vector3 offset = new Vector3(0, -0.1f, 0);

        try
        {
            parabolaDestination = target.transform.position + offset;
            ParabolaMove();
        }
        catch(NullReferenceException e)
        {
            Destroy(gameObject);
        }


    }

    // Update is called once per frame
    protected override void Update()
    {
        

    }
    

    public void ParabolaMove()
    {

        // y 축 이동 (위 아래로 움직이기)
        iTween.MoveBy(child, iTween.Hash("y", parabolaHeight, "time", moveTime / 2, "easeType", iTween.EaseType.easeOutQuad));
        iTween.MoveBy(child, iTween.Hash("y", -parabolaHeight, "time", moveTime / 2, "delay", moveTime / 2, "easeType", iTween.EaseType.easeInCubic));

        //iTween.RotateTo(child, iTween.Hash("onupdate", "ItweenUpdate"));

        // x, z 축 이동 (목적지로 이동하기)
        iTween.MoveTo(gameObject, iTween.Hash("position", parabolaDestination, "time", moveTime, "easeType", iTween.EaseType.linear));


        // 회전 포기

        //iTween.RotateTo(gameObject, iTween.Hash("position", parabolaDestination, "time", moveTime, "onupdate", "ItweenUpdate"));

        //iTween.RotateTo(gameObject, new Vector3(90f, 0, 0), 1.5f);

    }
    public void ItweenUpdate()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 30);
    }
}
