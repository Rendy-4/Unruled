using System;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    public float fadeSpeed;
    public float fadeAmount;
    float originalOpacity;

    Material material;

    public bool isFading = false;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        originalOpacity = material.color.a;
    }

    void Update()
    {
        if (isFading)
        {
            FadeNow();
        }
        else
        {
            ResetFade();
        }
    }

    void FadeNow()
    {
        Color currentColor = material.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, 
            Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
        material.color = smoothColor;
    }

    void ResetFade()
    {
        Color currentColor = material.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, 
            Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed * Time.deltaTime));
        material.color = smoothColor;
    }
}
