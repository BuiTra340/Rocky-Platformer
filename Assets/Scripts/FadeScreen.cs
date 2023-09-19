using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private bool fadeOut;
    private bool fadeIn;
    // Update is called once per frame
    private void Start()
    {
        fadeIn = true;
    }
    void Update()
    {
        if (fadeIn)
        {
            canvasGroup.alpha -= Time.deltaTime;
            if(canvasGroup.alpha <= 0)
            {
                fadeIn = false;
            }
        }
        //
        if (fadeOut)
        {
            canvasGroup.alpha += Time.deltaTime;
            if(canvasGroup.alpha >= 1)
            {
                fadeOut = false;
            }
        }
    }
    public void FadeOutScreen()
    {
        fadeOut = true;
    }
    public void FadeInScreen()
    {
        fadeIn = true;
    }
}
