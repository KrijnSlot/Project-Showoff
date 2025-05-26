using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealitySwap : MonoBehaviour
{
    [SerializeField] bool isOff = false;
    [SerializeField] float minOp;
    [SerializeField] SpriteRenderer oppacity;
    [SerializeField] BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        oppacity = GetComponent<SpriteRenderer>();
        if (isOff) oppacity.color = new Color(1f, 1f, 1f, minOp);
        else oppacity.color = new Color(1f, 1f, 1f, 1f);
        col.enabled = isOff;
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        PlayerPowers.swapReality += Swap;
    }

    private void OnDisable()
    {
        PlayerPowers.swapReality -= Swap;
    }


    void Swap()
    {
        isOff = !isOff;
        if (isOff) oppacity.color = new Color(1f, 1f, 1f, minOp);
        else oppacity.color = new Color(1f, 1f, 1f, 1f);
        col.enabled = isOff;
    }
}
