using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Bullet
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        MoveSpeed = 15f;
    }

    Vector3 arrowRotateOffset = new Vector3(0, 90f, 0);
    // Update is called once per frame
    protected override void Update()
    {
        // 날아가는 도중 타겟이 죽어서 없어지면 그냥 파괴
        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 dir = target.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(dir);
            transform.position += dir * MoveSpeed * Time.deltaTime;
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
