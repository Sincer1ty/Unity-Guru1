using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccineDrop : MonoBehaviour
{
    public GameObject vaccine;

    

    //IEnumerator dropTheItems()
    //{
    //    yield return new WaitForSeconds(0.3f);

    //    //빌런 자리에 소환
    //    Instantiate(vaccine, trans.position, Quaternion.identity);

    //    //파괴
    //    Destroy(this.gameObject);
    //}

    // Start is called before the first frame update
    //void Start()
    //{
    //    // 죽음 상태 처리 코루틴 실행
    //    StartCoroutine(DieProcess());        
    //}

    //IEnumerator DieProcess()
    //{
    //    // 캐릭터 컨트롤러 컴포넌트 비활성화
    //    cc.enabled = false;

    //    // 2초동안 기다린 후 자기 자신 제거
    //    yield return new WaitForSeconds(2f);
    //    Destroy(gameObject);
    //}

    // Update is called once per frame
    /*
    void Update()
    {
        // 게임 상태가 게임 중 상태가 아니면 업데이트 함수 종료
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "Vaccine")
        {
            Debug.Log("획득");
            Destroy(vaccine);
        }       
    }
    */
}
