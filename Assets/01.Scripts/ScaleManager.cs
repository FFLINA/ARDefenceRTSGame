using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    public static ScaleManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    internal float MoveSpeedFixForAR(float speed)
    {
        return speed * 0.15f;
    }
    internal void ScaleFixForAR(GameObject gameObject)
    {
        // 스케일을 0.1배 함 
        gameObject.transform.localScale *= 0.15f;
    }
}
