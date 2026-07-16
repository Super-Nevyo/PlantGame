using UnityEngine;

public class InputManager : MonoBehaviour
{
    public System.Action<Vector2> MoveAction;
    public System.Action InteractAction;
    public System.Action JumpAction;

    public static InputManager instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);

    }

    public void OnJump()
    {
        JumpAction?.Invoke();
    }
    public void OnMove(UnityEngine.InputSystem.InputValue value)
    {
        MoveAction?.Invoke(value.Get<Vector2>());
    }
    public void OnInteract()
    {
        InteractAction?.Invoke();
    }

}
