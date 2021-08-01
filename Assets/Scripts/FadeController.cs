using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public void FadeIn(float fadeouttime, System.Action nextEvent = null)
    {
        StartCoroutine(CoFadeIn(fadeouttime, nextEvent));
    }

    public void FadeOut(float fadeouttime, System.Action nextEvent = null)
    {
        StartCoroutine(CoFadeOut(fadeouttime, nextEvent));
    }


    // 투명 >> 불투명
    IEnumerator CoFadeIn(float fadeouttime, System.Action nextEvent = null)
    {
        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        Color tempcolor = sr.color;
        while (tempcolor.a < 1f)
        {
            tempcolor.a += Time.deltaTime / fadeouttime;
            sr.color = tempcolor;

            if (tempcolor.a >= 1f) tempcolor.a = 1f;

            yield return null;
        }
        sr.color = tempcolor;
        if (nextEvent != null) nextEvent();
    }

    // 불투명 >> 투명
    IEnumerator CoFadeOut(float fadeouttime, System.Action nextEvent = null)
    {
        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        Color tempcolor = sr.color;
        while (tempcolor.a > 0f)
        {
            tempcolor.a += Time.deltaTime / fadeouttime;
            sr.color = tempcolor;

            if (tempcolor.a >= 0f) tempcolor.a = 0f;

            yield return null;
        }
        sr.color = tempcolor;
        if (nextEvent != null) nextEvent();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

    