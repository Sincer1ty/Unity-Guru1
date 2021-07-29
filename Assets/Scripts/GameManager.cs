using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //플레이어 게임 오브젝트 변수
    GameObject player;

    //플레이어 무브 컴포넌트 변수
    PlayerController playerC;

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
        //게임의 상태를 실행 상태로 설정
        gState = GameState.Run;

        //플레이어 오브젝트를 검색
        player = GameObject.Find("Player");

        playerC = player.GetComponent<PlayerController>();
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
