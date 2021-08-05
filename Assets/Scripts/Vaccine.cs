using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vaccine : MonoBehaviour
{
    [System.NonSerialized]
    public int score = 1;

    public AudioSource collectSound;
    
    
    public void SetScoreValue(int score)
    {
        this.score = score;
    }
    

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Player")
        {
            collectSound.Play();
            //col.gameObject.GetComponent<ScoringSystem>().AddScore(score);
            //Destroy(gameObject);
            ReMoveFromWorld();
            GameObject.Find("GameManager").GetComponent<ScoringSystem>().score += 1;
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
