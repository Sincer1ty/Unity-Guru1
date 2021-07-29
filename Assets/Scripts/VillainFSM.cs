

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainFSM : MonoBehaviour
{
    // 빌런 상태 상수
    enum VillainState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    // 빌런 상태 변수
    VillainState v_state;

    // 플레이어 발견 범위
    public float findDistance = 8f;

    // 플레이어 트랜스폼
    Transform player;

    // 공격 가능 범위
    public float attackDistance = 2f;

    // 이동속도
    public float moveSpeed = 5f;

    // 캐릭터 컨트롤러 컴포넌트
    CharacterController cc;

    // 누적시간
    float currentTime = 0;

    // 공격 딜레이 시간
    float attackDelay = 2f;

    // 빌런 공격력
    public int attackPower = 3;

    // 초기 위치 저장용 변수
    Vector3 originPos;
    Quaternion originRot;

    // 이동 가능 범위
    public float moveDistance = 20f;

    // 빌런 체력
    public int hp = 15;



    void Start()
    {
        // 최초 빌런 상태는 idle
        v_state = VillainState.Idle;

        // 플레이어 트랜스폼 컴포넌트 받아오기
        player = GameObject.Find("Player").transform;

        // 캐릭터 플레이어 컴포넌트 가져오기
        cc = GetComponent<CharacterController>();

        // 초기 위치, 회전 값 저장
        originPos = transform.position;
        originRot = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        // 현재 상태 체크 >> 해당 상태별로 정해진 기능 수행
        switch (v_state)
        {
            case VillainState.Idle:
                Idle();
                break;
            case VillainState.Move:
                Move();
                break;
            case VillainState.Attack:
                Attack();
                break;
            case VillainState.Return:
                Return();
                break;
            case VillainState.Damaged:
                //Damaged();
                break;
            case VillainState.Die:
                //Die();
                break;
        }
    }

    void Idle()
    {
        // 플레이어와의 거리가 발견 범위보다 가까워질 경우 >> move
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            v_state = VillainState.Move;
        }
    }

    void Move()
    {

        // 현재 위치가 초기 위치에서 이동 가능 범위를 넘어갈 경우
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            // 현재 상태를 복귀로 전환
            v_state = VillainState.Return;

        }

        // 플레이어와의 거리가 공격 범위보다 멀 경우 >> 플레이어를 향해 이동
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            // 이동 방향 설정
            Vector3 dir = (player.position - transform.position).normalized;

            // 캐릭터 컨트롤러를 이용해 이동
            cc.Move(dir * moveSpeed * Time.deltaTime);

            // 플레이어를 향해 방향 전환
            transform.forward = dir;
        }

        // 그렇지 않다면 공격상태로 전환
        else
        {
            v_state = VillainState.Attack;

            // 누적 시간을 공격 딜레이 시간만큼 미리 진행
            currentTime = attackDelay;
        }
    }

    void Attack()
    {
        // 플레이어가 공격 범위 내에 존재 >> 플레이어 공격
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            // 일정한 시간마다 플레이어 공격
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                player.GetComponent<PlayerController>().DamageAction(attackPower);
                currentTime = 0;
            }
        }
        // 그렇지 않다면 이동상태로 전환
        else
        {
            v_state = VillainState.Move;
            currentTime = 0;
        }
    }

    void Return()
    {
        // 초기 위치에서의 거리가 0.1f 이상이면 초기 위치 쪽으로 이동
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);

            // 방향을 복귀 지점으로 전환
            transform.forward = dir;
        }
        // 그렇지 않다면 자신의 위치를 초기 위치로 조정 >> 대기 상태 전환
        else
        {
            // 위치, 회전 값을 초기 상태로 변환
            transform.position = originPos;
            transform.rotation = originRot;

            // hp 다시 회복

            v_state = VillainState.Idle;
        }
    }

    // 데미지 실행 함수
    public void HitVillain(int hitPower)
    {
        // 이미 피격, 사망, 복귀 상태라면 함수 종료
        if (v_state == VillainState.Damaged || v_state == VillainState.Die || v_state == VillainState.Return)
        {
            return;
        }
        
        // 플레이어의 공격력만큼 에너미 체력 감소
        hp -= hitPower;

        // 빌런 체력이 0보다 크면 피격 상태로 전환
        if (hp > 0)
        {
            v_state = VillainState.Damaged;
            Damaged();
        }
        // 그렇지 않다면 죽음 상태로 전환
        else
        {
            v_state = VillainState.Die;
            Die();
        }

    }

    void Damaged()
    {
        // 피격 상태 처리 코루틴 실행
        StartCoroutine(DamageProcess());
    }

    // 데미지 처리용 코루틴 함수
    IEnumerator DamageProcess()
    {
        // 피격 모션만큼 기다리기
        yield return new WaitForSeconds(0.5f);

        // 현재 상태를 이동 상태로 전환
        v_state = VillainState.Move;
    }

    void Die()
    {
        // 진행중인 피격 코루틴 중지
        StopAllCoroutines();

        // 죽음 상태 처리 코루틴 실행
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        // 캐릭터 컨트롤러 컴포넌트 비활성화
        cc.enabled = false;

        // 2초동안 기다린 후 자기 자신 제거
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
