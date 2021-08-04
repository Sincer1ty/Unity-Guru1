using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent : MonoBehaviour
{

    public VillainFSM eFSM;
    public void OnHit()
    {
        eFSM.HitEvent();
    }
}
