using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    // 옵션 메뉴 UI 오브젝트
    public GameObject optionUI;

    // 게임 오버 출력
    public GameObject gameOver;

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
        if (GameObject.Find("Player").GetComponent<PlayerController>().hp <= 0) // 배디엔딩씬 조건
        {
            gameOver.SetActive(true);
            GameObject.Find("BadEndingIntro").GetComponent<FadeController>().Fade();
        }


        if (GetComponent<ScoringSystem>().score == 10) // 해피엔딩씬 조건
        {
            GameObject.Find("HappyEndingIntro").GetComponent<FadeController>().Fade();

        }
    }

    // 옵션 메뉴 켜기
    public void OpenOptionWindow()
    {
        // 게임 상태를 pause로 변경
        gState = GameState.Pause;

        // 시간 멈춤
        Time.timeScale = 0;

        // 옵션 메뉴 창 활성화
        optionUI.SetActive(true);
    }

    // 옵션 메뉴 끄기(계속하기)
    public void CloseOptionWindow()
    {
        // 게임 상태를 run 상태로 변경
        gState = GameState.Run;

        // 시간을 1배로 되돌림
        Time.timeScale = 1.0f;

        // 옵션 메뉴 창 비활성화
        optionUI.SetActive(false);
    }

    // 게임 재시작(현재 씬 다시 로드)
    public void GameRestart()
    {
        // 시간을 1배로 되돌림
        Time.timeScale = 1.0f;

        // 현재 씬을 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 게임 종료
    public void GameQuit()
    {
        // 어플리케이션 종료
        Application.Quit();
    }
}
