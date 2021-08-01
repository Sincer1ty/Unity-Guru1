using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    public FadeController fader;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        fader.FadeOut(0.7f);
        yield return new WaitForSeconds(3f);
        fader.FadeIn(0.6f, () =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("");
        });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
