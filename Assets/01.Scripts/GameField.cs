using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    // 하나의 게임필드에 하나의 타워만 설치 가능
    public bool isBuildable { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        isBuildable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
