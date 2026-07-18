using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{
    public CameraStateMachine MyStateMachine;
    public Room CurrentRoom;
    public GameObject CameraPivot;
    [HideInInspector]
    public float ObjectiveRotation;
    [SerializeField] public float RotationSpeed;
    public float YOffset;
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
    private void FixedUpdate()
    {
        MyStateMachine.Execute();
    }
    public void ChangeCamRoom(Room newRoom)
    {
        if (CurrentRoom != null)
        ObjectiveRotation = CurrentRoom.attachedRoomDefaultRotations[Array.IndexOf(CurrentRoom.ConnectedRooms, newRoom)];
        CurrentRoom = newRoom;
        MyStateMachine.ChangeState(MyStateMachine.RoomsState);
    }
}
