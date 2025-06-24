using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpBobbingScript : MonoBehaviour
{
    Vector2 startPos;
    public float bobSpeed = 1f;
    public float bobHeight = 1f;
    private void Awake()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        Bobbing();
    }

    void Bobbing()
    {
        float newY = (startPos.y + 0.2f) + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        float newX = (startPos.x + 0.2f) + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector2(newX, newY);
    }
}
