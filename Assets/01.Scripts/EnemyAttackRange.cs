using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    // 적의 사정거리 안에 들어온 타워를 탐지해서
    // 부모인 에너미한테 전해준다

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Building") || other.gameObject.CompareTag("Crystal"))
        {
            transform.parent.GetComponent<Enemy>().SetNearTarget(other.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
