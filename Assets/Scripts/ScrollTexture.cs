using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 0.5f;
    private Renderer objRenderer;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector2 offset = new Vector2(Time.time * scrollSpeed, 0);
        objRenderer.material.mainTextureOffset = offset;
    }
}
