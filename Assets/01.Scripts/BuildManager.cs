using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // 생성된 크리스탈을 등지게 끔 다른 건물들의 방향을 회전
    // 건물에서 크리스탈 방향의 반대 (노말) 방향

    public static BuildManager Instance;
    private void Awake()
    {
        Instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Updatis called once per frame
    void Update()
    {

    }

    public Vector3 CrystalPosition { get; set; }

}
