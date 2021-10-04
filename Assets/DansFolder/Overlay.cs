using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    public Text mainTitle;
    public GameObject scoreStuff;

    void Start()
    {
        scoreStuff.SetActive(false);
        SetColorOfTextToTransparent(mainTitle, 0f);
        StartCoroutine(DisplayTextOverTime(mainTitle, 0.1f));
    }

    void SetColorOfTextToTransparent(Text text, float alpha)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }

    IEnumerator DisplayTextOverTime(Text text, float rate)
    {
        float holdRate = 0f;
        while(holdRate < 1)
        {
            holdRate += rate;
            SetColorOfTextToTransparent(text, holdRate);
            yield return null;
        }
        yield return new WaitForSeconds(1);

        while(holdRate > 0)
        {
            holdRate -= rate;
            SetColorOfTextToTransparent(text, holdRate);
            yield return null;
        }
        scoreStuff.SetActive(true);
    }
}
