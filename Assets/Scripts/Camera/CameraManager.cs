using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CameraStateMachine MyStateMachine;
    public Room CurrentRoom;
    public GameObject CameraPivot;
    public float ObjectiveRotation;
    [SerializeField] public float RotationSpeed;
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
        CurrentRoom = newRoom;
        MyStateMachine.ChangeState(MyStateMachine.RoomsState);
    }
}
