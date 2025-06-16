using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.WSA;

public class PlatformSwitch : UseAble
{
    [SerializeField]
    GameObject[] obstacle;

    public override void Activate()
    {
        foreach (GameObject go in obstacle) 
        {
            go.GetComponent<PlatformBase>().Activate();
        }
    }
}
