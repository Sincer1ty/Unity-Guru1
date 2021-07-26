using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //중력 변수
    public float gravity = -20.0f;

    //점프력
    public float jumpPower = 10.0f;

    //최대 점프 횟수
    public int maxJump = 2;

    //현재 점프 횟수
    int jumpCount = 0;

    //수직 속도 변수
    float yVelocity = 0;

    //속력 변수
    public float moveSpeed = 7.0f;

    //나의 캐릭터 콘트롤러
    CharacterController cc;

    //체력 변수
    public int hp;

    //최대 체력
    public int maxHp = 10;

    // Start is called before the first frame update
    void Start()
    {
        //캐릭터 컨트롤러 컴포넌트 받기
        cc = GetComponent<CharacterController>();

        //체력변수 초기화
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
