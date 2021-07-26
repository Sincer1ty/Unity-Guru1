using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    // 회전 속도 변수
    public float rotSpeed = 200f;

    // 회전 값 변수
    float mx = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자의 마우스 입력을 받아 물체 회전
        // 1. 마우스 입력
        float mouse_X = Input.GetAxis("Mouse X");

        // 1-1. 회전 값 변수에 마우스 입력 값만큼 미리 누적
        mx += mouse_X * rotSpeed * Time.deltaTime;

        // 2. 마우스 입력 값을 이용해 회전 방향 결정
        transform.eulerAngles= new Vector3(0, mx, 0);

    }
}
