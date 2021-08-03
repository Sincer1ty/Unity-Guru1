using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaccine : MonoBehaviour
{
    [System.NonSerialized]
    public int score = 1;
    
    
    public void SetScoreValue(int score)
    {
        this.score = score;
    }
    

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Player")
        {
            //col.gameObject.GetComponent<ScoringSystem>().AddScore(score);
            //Destroy(gameObject);
            ReMoveFromWorld();
        }
    }
    public void ReMoveFromWorld()
    {
        gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
       
    }
}
