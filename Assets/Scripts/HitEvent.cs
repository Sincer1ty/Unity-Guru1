using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent : MonoBehaviour
{

    public EnemyFSM eFSM;
    public void OnHit()
    {
        eFSM.HitEvent();
    }
}
