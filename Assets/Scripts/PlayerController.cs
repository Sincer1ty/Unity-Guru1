using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    //중력 변수
    public float gravity = -20.0f;

    //점프력
    public float jumpPower = 4.0f;

    //최대 점프 횟수
    public int maxJump = 6;

    //현재 점프 횟수
    int jumpCount = 0;

    //수직 속도 변수
    float yVelocity = 0;

    //속력 변수
    public float moveSpeed = 7.0f;

    //나의 캐릭터 콘트롤러
    CharacterController cc;

    [HideInInspector]
    //체력 변수
    public int hp;

    //최대 체력
    public int maxHp = 10;

    // 슬라이더 UI
    public Slider hpSlider;

    //회전 감도
    public float rotSpeed = 200.0f;

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
        // 게임 상태가 게임 중 상태가 아니면 업데이트 함수 종료
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //이동방향 설정
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        //이동방향 월드좌표 설정
        dir = transform.TransformDirection(dir);
        //플레이어가 땅에 착지시 현재 점프 횟수를 0으로 초기화
        //수직 속도 값(중력)을 다시 0으로 초기화
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            jumpCount = 0;
            yVelocity = 0;
        }

        //점프 키를 누를 시, 점프력을 수직 속도로 적용
        //단, 현재 점프 횟수가 최대 점프 횟수를 넘어가지 않아야 함
        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            jumpCount++;
            yVelocity = jumpPower;
        }

        //캐릭터의 수직속도(중력)을 적용
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        //이동방향으로 플레이어 이동
        //P = P0 + VT
        //transform.position += dir * moveSpeed * Time.deltaTime;
        Move();
        cc.Move(dir * moveSpeed * Time.deltaTime);

        LookAround();

        // 슬라이더 value를 체력 비율로 적용
        hpSlider.value = (float)hp / (float)maxHp;
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 moveInput = new Vector2(h, v);
        bool isMove = moveInput.magnitude != 0;
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = lookForward;
            //transform.position += moveDir * Time.deltaTime * 5f;

        }
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if(x<180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    // 플레이어 피격 함수
    public void DamageAction(int damage)
    {
        // 에너미의 공격력만큼 플레이어의 체력 감소
        hp -= damage;
    }
}
