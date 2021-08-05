using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{   
    //발사할 위치
    public Transform fireTransform;

    // 피격 이펙트 게임 오브젝트
    public GameObject bulletEffect;

    // 파티클 시스템 변수
    ParticleSystem ps;

    // 소독제 발사 효과 이펙트 
    public ParticleSystem sanitizerEffect;

    // 무기 공격력
    public int weaponPower = 5;

    //사정거리
    private float fireDistance = 50f;

    //public interface IDamageable
    //{
    //    void TakeHit(float damage, RaycastHit hit);
    //}

    void Start()
    {
        // 파티클 시스템 컴포넌트 가져오기
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    public void Shot()
    {
        //레이 생성
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        //충돌 정보 저장
        RaycastHit hit;

        //총알이 맞은 곳 저장
        Vector3 hitPosition = Vector3.zero;

        //레이캐스트
        if (Physics.Raycast(ray, out hit))
        {
            string layerName = LayerMask.LayerToName(hit.transform.gameObject.layer);

            //부딪힌 대상의 이름을 콘솔창에 출력
            print(layerName);
            
            //레이가 충돌한 경우

            ////충돌한 상대방으로부터 IDamageable 오브젝트 가져오기 시도
            //IDamageable target = hit.collider.GetComponent<IDamageable>();

            //레이에 부딪힌 대상의 레이어가 'Enemy'일 경우 데미지 함수 실행
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {         
                EnemyFSM eFSM = hit.transform.GetComponent<EnemyFSM>();
                //HitEnemy 함수 실행
                eFSM.HitEnemy(weaponPower);
            }

            // 피격 이펙트의 위치를 레이가 부딪한 지점으로 이동
            bulletEffect.transform.position = hit.point;

            // 피격 이펙트의 forward  방향을 레이가 부딪힌 지점의 노멀 벡터와 일치
            bulletEffect.transform.forward = hit.normal;
            

            ////위 과정 성공
            //if (target != null)
            //{
            //    target.TakeHit(weaponPower, hit);
            //}

            //레이 충돌 위치 저장
            hitPosition = hit.point;
        }
        else
        {
            //충돌하지 않았다면
            //최대 사정거리까지 갔을 때의 위치를 충돌 위치로 사용
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }
        //발사 이펙트 재생        
        sanitizerEffect.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 상태가 게임 중 상태가 아니면 업데이트 함수 종료
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }
        //// 마우스 왼쪽 버튼을 누르면 시선이 바라보는 방향으로 공격
        //// 마우스 왼쪽 버튼 입력
        if (Input.GetMouseButton(0))
        {
            //    // 레이 생성 후 발사될 위치, 진행 방향 설정
            //    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //    // 레이가 부딪힌 대상의 정보를 저장할 변수 생성
            //    RaycastHit hitInfo = new RaycastHit();

            //    // 레이 발사 후 만일 부딪힌 물체가 있으면 피격 이펙트 표시
            //    if (Physics.Raycast(ray, out hitInfo))
            //    {
            //        

            //        // 피격 이펙트를 플레이
            //        ps.Play();
            Shot();
        }
    }
}
