using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CameraStateMachine MyStateMachine;
    public Room CurrentRoom;
    public GameObject CameraPivot;
    public (Vector3, Quaternion, float) PreviousCamLocation;
    private void Awake()
    {
        MyStateMachine = new CameraStateMachine(this);
    }
    private void OnEnable()
    {
        MyStateMachine.Initialize(MyStateMachine.RoomsState);
    }
    private void OnDisable()
    {
        MyStateMachine.Disable();
    }
    public void ChangeCamRoom(Room newRoom)
    {
        CurrentRoom = newRoom;
        MyStateMachine.ChangeState(MyStateMachine.RoomsState);
    }
    public void SetCameraPosition(Vector3 pivot, Quaternion pivotRotation, float camDistance)
    {
        PreviousCamLocation = (Vector3.zero, Quaternion.identity, 0);
    }
}
