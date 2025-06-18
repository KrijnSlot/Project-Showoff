using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NoteTimer : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] float fallSpeed;
    [SerializeField] Transform playerPos;
    [SerializeField] float timer;
    [SerializeField] float timeActive = 0.2f;
    [SerializeField] SpriteRenderer renderer;
    bool On;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        timer = timeActive;
    }
    private void Update()
    {
        if(timer >0)
        timer -= Time.deltaTime;
        
    }

    public void Activate()
    {
        transform.position = playerPos.position + startPos;
        timer = timeActive;
        On = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (On)
        {
            transform.position -= new Vector3(0, fallSpeed, 0);
            if (timer < 0)
            {
                renderer.enabled = false;
                On = false;
            }
        }
    }
}
