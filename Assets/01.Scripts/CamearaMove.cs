using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamearaMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotSpeed = 200f;
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
        rX = Mathf.Clamp(rX, -30f, 30f);


        rX += mouseY * Time.deltaTime * rotSpeed;
        rX = Mathf.Clamp(rX, -30f, 30f);
        rY += mouseX * Time.deltaTime * rotSpeed;

        transform.rotation = Quaternion.Euler(-rX, rY, 0);
    }

    private void MoveUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();
        dir = Camera.main.transform.TransformDirection(dir);

        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
