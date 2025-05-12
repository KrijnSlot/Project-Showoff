using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoinScript : MonoBehaviour
{
    [SerializeField] int playerCount = 0;
    [SerializeField] List<Transform> spawn = new List<Transform>();
    [SerializeField] List<GameObject> prefab = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < playerCount; i++)
        {
            Instantiate(prefab[i], spawn[i].position, spawn[i].rotation);
        }
    }
}
