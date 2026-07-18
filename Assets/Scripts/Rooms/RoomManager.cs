using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // TODO: once there is a save system, this should be set on load to the saved room
    [SerializeField] private Room currentRoom;
    [SerializeField] private string LayerName;
    private Room[] _connectedRooms;
    private RaycastHit _hit;

    void Start()
    {
        SetRoom(currentRoom);
        Debug.Log(LayerMask.GetMask(LayerName));
    }
    private void FixedUpdate()
    {
        foreach (Room room in _connectedRooms)
        {
            if (Physics.BoxCast(room.RoomCenter + room.HalfExtent.y * Vector3.up, room.HalfExtent, Vector3.down, out _hit, Quaternion.identity, 10, LayerMask.GetMask("Player")))
            {
                Debug.Log("Cast success");
                SetRoom(room);
            }
        }
    }

    private void SetRoom(Room newRoom)
    {
        currentRoom = newRoom;
        _connectedRooms = new Room[currentRoom.ConnectedRooms.Length];
        for (int i = 0; i < _connectedRooms.Length; i++)
        {
            _connectedRooms[i] = currentRoom.ConnectedRooms[i];
        }
        EventManager.RoomChanged(currentRoom);
    }

    //private void OnDrawGizmos()
    //{
    //    foreach (Room room in _connectedRooms)
    //    {
    //        if (room != null)
    //        Gizmos.DrawWireCube(room.RoomCenter, 2*room.HalfExtent);
    //    }
    //}

}
