using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    Vector2 beginPos;
    public float direction;
    public float speed;
    void Start()
    {
        beginPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > beginPos.x - 2.5f)
        {
            direction = -1;
        }
        else if (transform.position.x < beginPos.x + 2.5f)
        {
            direction = 1;
        }
        Vector3 movement = Vector3.right * direction * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
