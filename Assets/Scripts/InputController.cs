using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static InputController Instance;
    public PlayerInput playerInput;
    void Start()
    {
        if (Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }
        playerInput = transform.GetComponent<PlayerInput>();
    }
}
