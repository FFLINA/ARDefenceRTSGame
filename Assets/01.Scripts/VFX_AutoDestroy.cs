using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_AutoDestroy : MonoBehaviour
{
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
