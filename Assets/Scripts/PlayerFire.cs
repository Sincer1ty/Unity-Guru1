using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    // 피격 이펙트 오브젝트
    public GameObject bulletEffect;

    // 피격 이펙트 파티클 시스템
    ParticleSystem ps;

    // 소독제 발사 효과 이펙트 
    public GameObject sanitizerEffect;




    void Start()
    {
        // 피격 이펙트 오브젝트에서 파티클 시스템 컴포넌트 가져오기
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 왼쪽 버튼을 누르면 시선이 바라보는 방향으로 공격
        // 마우스 왼쪽 버튼 입력
        if (Input.GetMouseButton(0))
        {
            // 레이 생성 후 발사될 위치, 진행 방향 설정
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            // 레이가 부딪힌 대상의 정보를 저장할 변수 생성
            RaycastHit hitInfo = new RaycastHit();
        
            // 레이 발사 후 만일 부딪힌 물체가 있으면 피격 이펙트 표시
            if(Physics.Raycast(ray, out hitInfo))
            {
                // 피격 이펙트의 위치를 레이가 부딪한 지점으로 이동
                bulletEffect.transform.position = hitInfo.point;

                // 피격 이펙트의 forward  방향을 레이가 부딪힌 지점의 법선 벡터와 일치
                bulletEffect.transform.forward = hitInfo.normal;

                // 피격 이펙트를 플레이
                ps.Play();

                // 소독제 이펙트 실시
                StartCoroutine(San_EffectOn(0.05f));

            }
        }

        // 소독제 이펙트 코루틴 함수
        IEnumerator San_EffectOn(float duration)
        {
            //이펙트 오브젝트 활성화
            sanitizerEffect.SetActive(true);

            // 지정 시간만큼 기다리기
            yield return new WaitForSeconds(duration);

            // 이펙트 오브젝트 비활성화
            sanitizerEffect.SetActive(false); 
        }
    }
}
