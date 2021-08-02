using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
    public static int theScore;

    private void Start()
    {
        theScore = 0;
    }

    void Update()
    {
        // 백신 얻으면 UI의 개수 1증가
        scoreText.GetComponent<Text>().text = "" + theScore;

    }
}
