using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    PlayerPowers power = null;

    BreakablePlatform platform = null;
    bool isActive = false;
    PlayerPowers.PlayerSizes lastSize;
    private void Awake()
    {
        platform = GetComponentInParent<BreakablePlatform>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1" || collision.tag == "Player2")
        {
            isActive = true;
            if (power == null)
            {
                power = collision.GetComponent<PlayerPowers>();
            }
            if (power != null)
            {

                BreakStart();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player1" || collision.tag == "Player2") isActive = false;
    }
    private void Update()
    {
        if (isActive && power != null && lastSize != power.currentSize)
        {
            BreakStop();
            BreakStart();
        }
    }

    private void BreakStart()
    {
        if (power.currentPower == PlayerPowers.Powers.sizeManip)
        {
            lastSize = power.currentSize;
            if (power.currentSize == PlayerPowers.PlayerSizes.big)
            {
                platform.Activate(1);
            }
            else if (power.currentSize == PlayerPowers.PlayerSizes.normal)
            {
                platform.Activate(power.normalSizeBreakableTimeIncrease);
            }
        }
    }

    private void BreakStop()
    {
        platform.DeActivate();
    }
}
