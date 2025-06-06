using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatforms : MonoBehaviour
{
    [SerializeField] private float rotationDelay = 0.1f;
    [SerializeField] private  bool autoRotate;
    private float time = 0.0f;

    void Update()
    {
        if (autoRotate)
        {
        if (time >= rotationDelay)
        {
            time = time - rotationDelay;

            AutoRotatePlatform();
        }
        time += Time.deltaTime;
        }
    }

    public void Rotate()
    {
        if(autoRotate == false)
        {
        transform.Rotate(0, 0, 180);
        }
    }


    private void AutoRotatePlatform()
    {
        transform.Rotate(0, 0, 180);
    }
}
