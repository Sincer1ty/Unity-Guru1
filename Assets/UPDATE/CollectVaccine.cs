using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectVaccine : MonoBehaviour
{
    public AudioSource collectSound;
    void OnTriggerEnter(Collider other)
    {
        // 백신 얻으면 효과음 나면서 UI의 개수 1증가
        collectSound.Play();
        ScoringSystem.theScore += 1;
        Destroy(gameObject);
    }
}
