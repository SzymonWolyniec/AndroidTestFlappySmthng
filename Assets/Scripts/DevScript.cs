using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DevScript : MonoBehaviour
{
    public TextMeshProUGUI FPSDebugText;
    private float frequency = 1.0f;

    void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(FPS());
    }

    private IEnumerator FPS()
    {
        for (; ; )
        {
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it

            FPSDebugText.text = "FPS: " + Mathf.RoundToInt(frameCount / timeSpan).ToString();



        }
    }
}
