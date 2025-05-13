using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public Vector2 beginPos;
    public float moveDistance = 2.5f;
    public float speed = 2f;
    public int direction = 1;

    void Start()
    {
        beginPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (transform.position.x >= beginPos.x + moveDistance)
        {
            direction = -1;
        }
        else if (transform.position.x <= beginPos.x - moveDistance)
        {
            direction = 1;
        }
    }
}
