using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public Text scoreText;
    public int score;
    

    private void Start()
    {
        score = 0;
       
    }
    public void AddScore(int scoreValue)
    {
        
    }
    
    void Update()
    {


        scoreText.text = score.ToString();
        
    

    }
    
}
