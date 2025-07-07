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
        oppacity = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (isOff) oppacity.color = new Color(oppacity.color.r, oppacity.color.g, oppacity.color.b, minOp);
        else oppacity.color = new Color(oppacity.color.r, oppacity.color.g, oppacity.color.b, 1f);
        col.enabled = !isOff;
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
        if (isOff) oppacity.color = new Color(oppacity.color.r, oppacity.color.g, oppacity.color.b, minOp);
        else oppacity.color = new Color(oppacity.color.r, oppacity.color.g, oppacity.color.b, 1f);
        col.enabled = !isOff;
    }
}
