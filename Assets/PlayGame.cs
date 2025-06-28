using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayGame : MonoBehaviour
{
    PlayerInput input;
    Animator anim;
    [SerializeField] string scene; // this way you can add paste the scene in the string to open the preffered scene

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("initial call");
        if (context.performed)
        {
            Debug.Log("preformed state");
            StartCoroutine(PlayAnimation(2f)); // do anim.length instead of 2f
        }
    }

    private IEnumerator PlayAnimation(float animationLength)
    {
        Debug.Log("animation started");
        yield return new WaitForSeconds(animationLength);
        Debug.Log("Loading scene...");
        //SceneManager.LoadScene(scene); // uncomment when animations are implemented
    }
}
