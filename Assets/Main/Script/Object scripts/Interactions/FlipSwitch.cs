using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSwitch : MonoBehaviour
{
    Animator leverAnim;

    private void Awake()
    {
        leverAnim = this.GetComponent<Animator>();
    }

    public void Flip()
    {
        if (leverAnim.GetBool("isOpened") == false)
            leverAnim.SetBool("isOpened", true);
        else
            leverAnim.SetBool("isOpened", false);
    }
}
