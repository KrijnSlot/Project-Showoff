using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LahonTestingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SizeManipulation();   
    }

    void SizeManipulation()
    {
        Vector3 pScale = transform.localScale;
        float scaleSpeed = 0.01f;

        if (Input.GetKey(KeyCode.R) && pScale.x <= 5)
        {
            pScale += new Vector3(scaleSpeed, scaleSpeed, 0);
            Debug.Log("increasing size");
        }
        if (Input.GetKey(KeyCode.T) && pScale.x >= 0.25f)
        {
            pScale -= new Vector3(scaleSpeed, scaleSpeed, 0);
            Debug.Log("decreasing size");
        }
        // puts player back to a scale of 1
        if (Input.GetKey(KeyCode.Y))
        {
            if (pScale.x >= 1.001f)
            {
                pScale -= new Vector3(scaleSpeed, scaleSpeed, 0);
            }
            else if (pScale.x <= 0.999f)
            {
                pScale += new Vector3(scaleSpeed, scaleSpeed, 0);
            }
        }
        transform.localScale = pScale;
    }
}
