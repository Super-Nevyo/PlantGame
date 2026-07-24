using UnityEngine;
[CreateAssetMenu(fileName = "Room", menuName = "Scriptable Objects/Room")]
public class Room : ScriptableObject
{
    public string Name;
    public bool CanCamRotate;
    public Room[] ConnectedRooms;
    public float[] attachedRoomDefaultRotations;
    public Vector3 HalfExtent;
    public float CamDefaultRotation;
    public Vector3 RoomCenter;
}
