using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // 종료 버튼을 누르면 게임 종료
    public void Quitbutton()
    {
        Application.Quit();
    }

    // 다음 버튼을 누르면 게임 씬으로 이동
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene_re");
    }
}
