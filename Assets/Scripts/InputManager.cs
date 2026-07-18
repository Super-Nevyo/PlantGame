using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    public static InputManager instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);

    }

    public void OnJump()
    {
        EventManager.OnJump();
    }
    public void OnMove(UnityEngine.InputSystem.InputValue value)
    {
        EventManager.OnMove(value.Get<Vector2>());
    }
    public void OnLook(UnityEngine.InputSystem.InputValue value)
    {
        EventManager.OnLook(value.Get<float>());
    }
    public void OnInteract()
    {
        EventManager.OnInteract();
    }

}
