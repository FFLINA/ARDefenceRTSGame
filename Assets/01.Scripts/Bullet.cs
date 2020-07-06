using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bullet 은 데미지, 타겟을 가지고 있다

    // Bullet은 지정받은 타겟을 향해 고속으로 날라가서 타겟과 부딪히면 타겟에게 데미지를 전달
    protected int attackPower = 35;
    protected float moveSpeed = 50f;

    protected GameObject target;
    public void SetTarget(GameObject towersTarget)
    {
        target = towersTarget;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    float curTime = 0;
    // Update is called once per frame
    protected virtual void Update()
    {
        //curTime += Time.deltaTime;

        // 날아가는 도중 타겟이 죽어서 없어지면 그냥 파괴
        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 dir = target.transform.position - transform.position;
            //transform.rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.LookRotation(dir);
            //transform.position = Vector3.Lerp(transform.position, target.transform.position, curTime);
            transform.position += dir * moveSpeed * Time.deltaTime;
        }

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // other은 Model이기 때문에 그 parent가 Enemy
            //타겟에게 데미지를 전달
            GameObject enemy = other.gameObject.transform.parent.gameObject;
            enemy.GetComponent<Enemy>().HitBullet(attackPower);
            Destroy(gameObject);
        }
    }

}
