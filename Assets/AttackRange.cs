using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    // OnTriggerEnter 된 Enemy의 정보를 부모인 Tower한테 보낸다

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        // 사정거리에 적이 들어오면 그 건물의 타겟리스트에 추가
        if(other.gameObject.CompareTag("Enemy"))
        {
            transform.parent.GetComponent<Build>().AddTarget(other.transform.parent.gameObject);

            other.transform.parent.gameObject.GetComponent<Enemy>().SetAttacker(transform.parent.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 사정거리 밖으로 나가면 타겟리스트에서 삭제
        if (other.gameObject.CompareTag("Enemy"))
        {
            transform.parent.GetComponent<Build>().RemoveTarget(other.transform.parent.gameObject);
        }
    }
}
