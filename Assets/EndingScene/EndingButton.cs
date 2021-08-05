using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingButton : MonoBehaviour
{
    // 종료 버튼을 누르면 게임 종료
    public void Quitbutton()
    {
        Application.Quit();
    }

    // 처음으로 버튼을 누르면 처음 메뉴 씬으로 이동
    public void Restartbutton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
