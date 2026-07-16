using UnityEngine;

public static class EventManager
{
    public static System.Action EnterInteractStateEvent;
    public static System.Action NightEvent;
    public static System.Action<Room> RoomChangeEvent;

    public static void InteractionHappened()
    {
        EnterInteractStateEvent?.Invoke();
    }
    public static void DoNight()
    {
        NightEvent?.Invoke();
    }
    public static void RoomChanged(Room NewRoom)
    {
        RoomChangeEvent?.Invoke(NewRoom);
    }
}
