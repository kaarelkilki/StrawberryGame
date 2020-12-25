using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour
{
    // the image you want to fade, assign in inspector
    public Image imgLeft;
    public Image imgRight;

    void Start()
    {
        // fades the image out on start
        StartCoroutine(FadeImage(true));
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 3 second backwards
            for (float i = 3; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                imgLeft.color = new Color(1, 1, 1, i);
                imgRight.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
