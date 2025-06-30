
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class EnableConfine : MonoBehaviour
{
    [SerializeField] CinemachineConfiner2D confiner;

    private void Awake()
    {
        confiner.enabled = true;
    }
}
