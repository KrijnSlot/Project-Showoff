using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] float raycastReach = 0.5f;
    [SerializeField] List<UseAble> useAble = new List<UseAble>();
    [SerializeField] PlayerPowers powers;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        CheckForBigPlayer();
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
                for (int i = 0; i < useAble.Count; i++)
                {
                    if (!useAble[i].isOn) useAble[i].Activate();
                    useAble[i].isOn = true;
                }
            }
        }
        else
        {
            if(powers != null)
            {
                powers = null;
            }
            for (int i = 0; i < useAble.Count; i++)
            {
                if (useAble[i].isOn) useAble[i].Activate();
                useAble[i].isOn = false;
            }
        }
    }
}
