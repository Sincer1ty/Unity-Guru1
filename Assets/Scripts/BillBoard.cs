using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    // 메인 카메라의 전방방향, 나의 전방방향 일치 


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 메인 카메라의 전방방향, 나의 전방방향 일치 
        transform.forward = Camera.main.transform.forward;
    }
}
