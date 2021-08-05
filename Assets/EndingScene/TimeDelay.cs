using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDelay : MonoBehaviour
{
    public int second = 15;

    public GameObject RestartButton;
    public GameObject QuitButton;
    // Start is called before the first frame update
    void Start()
    {
        RestartButton.SetActive(false);
        QuitButton.SetActive(false);

        StartCoroutine(Timedelay());
    }

    IEnumerator Timedelay()
    {
        yield return new WaitForSeconds(second);

        RestartButton.SetActive(true);
        QuitButton.SetActive(true);
    }
}
