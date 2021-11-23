using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFlashing : MonoBehaviour
{
    public TextMeshProUGUI textToFlashing;
    private float flashingStep = 0.015f;

    private bool flashing = false;
    private Color baseColor;


    private void Awake()
    {
        baseColor = new Color(textToFlashing.color.r, textToFlashing.color.g, textToFlashing.color.b, textToFlashing.color.a);
    }

    private void OnEnable()
    {
        textToFlashing.color = baseColor;
        flashing = true;
        StartCoroutine(Flashing());
    }

    private void OnDisable()
    {
        flashing = false;
    }

    IEnumerator Flashing()
    {
        while (flashing)
        {
            for (float i = 1; i >= 0; i = i - flashingStep)
            {
                textToFlashing.color = new Color(textToFlashing.color.r, textToFlashing.color.g, textToFlashing.color.b, i);

                yield return new WaitForFixedUpdate();
            }

            for (float i = 0; i <= 1; i = i + flashingStep)
            {
                textToFlashing.color = new Color(textToFlashing.color.r, textToFlashing.color.g, textToFlashing.color.b, i);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
