
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockZPos : MonoBehaviour
{
    void Update()
    {
            transform.position = new Vector3(transform.position.x,transform.position.y,-10);
    }
}
