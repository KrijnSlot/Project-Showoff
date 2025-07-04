using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAble : MonoBehaviour
{
    public bool isOn = false;
    public virtual void Activate()
    {
        isOn = !isOn;
    }

    public virtual void Activate(float numb)
    {
        isOn = !isOn;
    }

    public virtual void DeActivate()
    {

    }
}

