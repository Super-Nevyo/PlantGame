using UnityEngine;

public static class EventManager
{
    // game state actions
    public static System.Action EnterInteractStateEvent;
    public static System.Action ExitInteractStateEvent;
    public static System.Action NightEvent;
    public static System.Action<Room> RoomChangeEvent;

    // input actions
    public static System.Action<Vector2> MoveAction;
    public static System.Action InteractAction;
    public static System.Action JumpAction;
    public static System.Action<float> LookAction;

    // game state actions
    public static void InteractionHappened()
    {
        EnterInteractStateEvent?.Invoke();
    }
    public static void ExitInteract()
    {
        ExitInteractStateEvent?.Invoke();
    }
    public static void DoNight()
    {
        NightEvent?.Invoke();
    }
    public static void RoomChanged(Room NewRoom)
    {
        RoomChangeEvent?.Invoke(NewRoom);
    }
    // input actions
    public static void OnJump()
    {
        JumpAction?.Invoke();
    }
    public static void OnMove(Vector2 value)
    {
        MoveAction?.Invoke(value);
    }
    public static void OnLook(float value)
    {
        LookAction?.Invoke(value);
    }
    public static void OnInteract()
    {
        InteractAction?.Invoke();
    }
    
}
