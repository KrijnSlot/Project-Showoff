using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObj : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] float rotationTime;

    Coroutine _turnCoroutine;

    private PlayerMovemenet player;

    bool _facingRight;
    // Start is called before the first frame update
    void Awake()
    {
        player = playerPos.gameObject.GetComponent<PlayerMovemenet>();
        _facingRight = player.facingRight;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.position;
    }

    public void CallTurn()
    {
        //_turnCoroutine = StartCoroutine(FlipYLerp());

        LeanTween.rotateY(gameObject, EndRotation(), rotationTime).setEaseInOutSine();
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation = EndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < rotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotation, (elapsedTime/rotationTime));
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
            yield return null;
        }
    }

    private float EndRotation()
    {
        _facingRight = !_facingRight;
        if (!_facingRight)
        {
            return 180;
        }
        else
        {
            return 0;
        }
    }
}
