using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatforms : PlatformBase
{
    [SerializeField] private float rotationDelay = 0.1f;
    [SerializeField] private bool autoRotate;
    [SerializeField] float time = 0.0f;
    [SerializeField] float rotationSpeed = 1;
    float rotation;
    [SerializeField] bool rotate = false;
    void Update()
    {
        if (autoRotate)
        {
            if (time >= rotationDelay)
            {
                rotate = true;
            }
            time += Time.deltaTime;
            if (rotate)
                AutoRotatePlatform();
        }
        else if (rotate)
        {
            Rotate();
        }
    }

    public override void Activate()
    {
        print("heeee");
        rotate = true;
    }

    public void Rotate()
    {
        transform.Rotate(0, 0, rotationSpeed);
        rotation += rotationSpeed;
        if (rotation >= 180)
        {
            rotate = false;
            rotation = 0.0f;
        }
    }


    private void AutoRotatePlatform()
    {
        transform.Rotate(0, 0, rotationSpeed);
        rotation += rotationSpeed;
        if(rotation >= 180)
        {
            time = 0.0f;
            rotate = false;
            rotation = 0.0f;
        }
        
    }
}
