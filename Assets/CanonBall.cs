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

    protected override void Start()
    {
        attackPower = 150;
        moveSpeed = 10f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 dir = target.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(dir);
            transform.position += dir * moveSpeed * Time.deltaTime;
        }

    }
    
    // y 축 이동을 담당하는 오브젝트
    public GameObject child;

    // 올라갈 높이
    public float parabolaHeight;

    // 최종 목적지
    public Vector3 parabolaDestination;
        
    // 이동까지 걸리는 시간
    public float moveTime;

    public void OnClickMove()
    {
        
        // y 축 이동 (위 아래로 움직이기)
        iTween.MoveBy(child, iTween.Hash("y", parabolaHeight, "time", moveTime / 2, "easeType", iTween.EaseType.easeOutQuad));
        iTween.MoveBy(child, iTween.Hash("y", -parabolaHeight, "time", moveTime / 2, "delay", moveTime / 2, "easeType", iTween.EaseType.easeInCubic));

        // x, z 축 이동 (목적지로 이동하기)
        iTween.MoveTo(gameObject, iTween.Hash("position", parabolaDestination, "time", moveTime, "easeType", iTween.EaseType.linear, "oncomplete", "Comeback"));
    }
}
