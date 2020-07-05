using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamearaMove : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float rotSpeed = 400f;
    float rX, rY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
        RotateUpdate();
    }

    private void RotateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rX += mouseY * Time.deltaTime * rotSpeed;
        rX = Mathf.Clamp(rX, -60f, 60f);
        rY += mouseX * Time.deltaTime * rotSpeed;

        transform.rotation = Quaternion.Euler(-rX, rY, 0);
    }

    private void MoveUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float u = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            u = -1;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            u = 1;
        }

        Vector3 dir = new Vector3(h, u, v);
        dir.Normalize();
        dir = Camera.main.transform.TransformDirection(dir);

        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
