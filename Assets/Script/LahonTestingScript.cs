using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
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
        Debug.Log(sizeCycle);
    }

    [Header("Rescaling Power")]
    int sizeCycle = 1;
    [SerializeField] float bigmode, normalMode, smallMode, scaleSpeed, growthHeight;
    [SerializeField] LayerMask mask;
    void SizeManipulation()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, growthHeight, mask);
        print(hit.collider);
        Vector3 pScale = transform.localScale;
        float scaleSpd = scaleSpeed;

        if (Input.GetKeyDown(KeyCode.R))
        {
            sizeCycle++;
        }
        if (sizeCycle == 1 && pScale.x <= normalMode - 0.01f)
        {
            pScale += new Vector3(scaleSpd, scaleSpd, 0);
        }
        else if (sizeCycle == 1 && pScale.x >= normalMode + 0.01f)
        {
            pScale -= new Vector3(scaleSpd, scaleSpd, 0);
        }
        if (sizeCycle == 2 && pScale.x <= bigmode - 0.01f && !hit)
        {
            pScale += new Vector3(scaleSpd, scaleSpd, 0);
        }
        else if (sizeCycle == 2 && hit && pScale.x <= bigmode - 0.5)
            sizeCycle--;
        if (sizeCycle == 3 && pScale.x >= smallMode + 0.01f)
        {
            pScale -= new Vector3(scaleSpd, scaleSpd, 0);
        }

        if (sizeCycle >= 4)
            sizeCycle = 1;
        transform.localScale = pScale;
    }
}
