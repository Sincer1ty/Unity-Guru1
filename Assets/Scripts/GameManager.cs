using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //게임 상태 변수
    public enum GameState
    {
        Ready,
        Run,
        Pause,
        GameOver
    }

    //게임 상태 변수
    public GameState gState;

    // 텍스트 변수
    public Text stateLable;

    //플레이어 게임 오브젝트 변수

    //GameObject player;

    //플레이어 무브 컴포넌트 변수
    //PlayerController playerC;

    //싱글턴
    public static GameManager gm;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 게임 초기 상태 = 준비상태 
        gState = GameState.Ready;

        //게임의 상태를 실행 상태로 설정
        //gState = GameState.Run;

        // 게임 시작 코루틴 함수 실행
        StartCoroutine(GameStart());

        //플레이어 오브젝트를 검색
        //player = GameObject.Find("Player");

        //playerC = player.GetComponent<PlayerController>();
    }

    IEnumerator GameStart()
    {
        // Ready 문구 표시
        stateLable.text = "Ready";

        // Ready 문구 색상 : 주황색
        stateLable.color = new Color32(234, 182, 13, 255);

        // 2초 대기
        yield return new WaitForSeconds(2.0f);

        // Go 문구로 변경
        stateLable.text = "Go!";

        // 0.5초간 대기
        yield return new WaitForSeconds(0.5f);

        // Go 문구 제거
        stateLable.text = "";

        // 게임 상태 전환 : 준비 -> 실행 
        gState = GameState.Run;
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
