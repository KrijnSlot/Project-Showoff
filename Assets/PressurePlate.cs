using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] float raycastReach = 0.5f;
    [SerializeField] UseAble useAble;
    [SerializeField] PlayerPowers powers;
    // Start is called before the first frame update
    void Awake()
    {
        useAble = GetComponent<UseAble>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForBigPlayer();
        Debug.Log(useAble.isOn);
    }

    void CheckForBigPlayer()
    {
        RaycastHit2D pressurePlatePressed = Physics2D.Raycast(transform.position, transform.up, raycastReach, mask);
        if (pressurePlatePressed)
        {
            if(powers == null)
            {
                powers = pressurePlatePressed.transform.GetComponent<PlayerPowers>();
            }
            if (powers != null && powers.currentSize == PlayerPowers.PlayerSizes.big)
            {
                Debug.Log("supposed to be on");
                if(!useAble.isOn) useAble.Activate();
                useAble.isOn = true;
            }
        }
        else
        {
            if(powers != null)
            {
                powers = null;
            }
            Debug.Log("supposed to be off");
            if (useAble.isOn) useAble.Activate();
            useAble.isOn = false;
        }
    }
}
