using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    public Vector2 scrollSpeed = new Vector2(0.1f, 0.0f); // Forward-only scroll speed
    public float bobAmplitude = 0.2f;                     // Vertical motion range
    public float bobFrequency = 1f;                       // Speed of bobbing (Hz)

    private Renderer rend;
    private Vector2 currentOffset = Vector2.zero;
    private Vector3 startPos;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startPos = transform.position;
    }

    void Update()
    {
        // Scroll texture forward only
        currentOffset += scrollSpeed * Time.deltaTime;
        rend.material.mainTextureOffset = currentOffset;

        // Vertical bobbing using sine wave
        float bobOffset = Mathf.Sin(Time.time * Mathf.PI * 2f * bobFrequency) * bobAmplitude;
        transform.position = startPos + new Vector3(0f, bobOffset, 0f);
    }
}

