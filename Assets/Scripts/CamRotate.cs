using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{

    // 회전 속도 변수
    public float rotSpeed = 200f;

    // 회전 값 변수
    float mx = 0;
    float my = 0;

    public float low_angleLimit = 30.0f;
    public float high_angleLimit = 30.0f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자의 마우스 입력을 받아 물체 회전
        // 1. 마우스 입력
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        // 1-1. 회전 값 변수에 마우스 입력 값만큼 미리 누적
        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;

        // 1-2. 마우스 상하 이동 회전 변수(my) 값 제한
        my = Mathf.Clamp(my, -low_angleLimit, high_angleLimit);

        // 2. 마우스 입력 값을 이용해 회전 방향 결정
        transform.eulerAngles = new Vector3(-my, mx, 0);

        /*
        // 3. 회전 방향으로 물체 회전
        // r = r0 + vt
        transform.eulerAngles += dir * rotSpeed * Time.deltaTime;

        // 4. x 축 회전(상하 회전) 값을 -90 ~ 90도 사이로 제한
        Vector3 rot = transform.eulerAngles;
        rot.x = Mathf.Clamp(rot.x, -90f, 90f);
        transform.eulerAngles = rot;
        */


    }
}
