using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleInteraction : MonoBehaviour
{

    public void InteractHandle(string tag)
    {
        Debug.Log("______tag" + tag);
        switch (tag)
        {
            case ("MovingPlatform"): GetComponent<MovingPlatformScript>().enabled = true; break;
            case ("Button"): this.gameObject.SetActive(false); break;
        }

    }
}
