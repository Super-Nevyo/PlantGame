using UnityEngine;

public class CSRooms : IState
{
    private CameraManager cameraManager;
    private Room room;
    private float rotationDirection;
    private bool CamPivotMoved;
    public CSRooms(CameraManager cam)
    {
        cameraManager = cam;
    }
    public void Enter()
    {
        CamPivotMoved = false;
        if (room == null)
        {
            room = cameraManager.CurrentRoom;
            cameraManager.ObjectiveRotation = room.CamDefaultRotation;
        }
        else if (room != cameraManager.CurrentRoom)
        {
            room = cameraManager.CurrentRoom;
        }
        EventManager.EnterInteractStateEvent += EnterInteractState;
        EventManager.LookAction += OnRotate;
        EventManager.RoomChangeEvent += cameraManager.ChangeCamRoom;
    }

    public void Exit()
    {
        OnRotate(0);
        EventManager.EnterInteractStateEvent -= EnterInteractState;
        EventManager.LookAction -= OnRotate;
        EventManager.RoomChangeEvent -= cameraManager.ChangeCamRoom;
    }

    public void Update()
    {
        cameraManager.ObjectiveRotation += rotationDirection * cameraManager.RotationSpeed * Time.fixedDeltaTime;
        if (cameraManager.ObjectiveRotation < 0) cameraManager.ObjectiveRotation += 360;
        if (cameraManager.ObjectiveRotation > 360) cameraManager.ObjectiveRotation -= 360;
        if (!CamPivotMoved)
        {
            cameraManager.CameraPivot.transform.position = Vector3.Lerp(cameraManager.CameraPivot.transform.position, room.RoomCenter, 0.2f);
            if ((cameraManager.CameraPivot.transform.position - room.RoomCenter).sqrMagnitude < 0.1)
            {
                Debug.Log("PivotLocked");
                cameraManager.CameraPivot.transform.position = room.RoomCenter;
                CamPivotMoved = true;
            }
        }
        cameraManager.CameraPivot.transform.rotation = Quaternion.Slerp(cameraManager.CameraPivot.transform.rotation, Quaternion.Euler(0, Mathf.Atan2(room.HalfExtent.x * Mathf.Cos(cameraManager.ObjectiveRotation * Mathf.Deg2Rad), room.HalfExtent.z * Mathf.Sin(cameraManager.ObjectiveRotation * Mathf.Deg2Rad)) * Mathf.Rad2Deg, 0), 0.2f);
        // this will hopefully make the camera move in an ellipse
        cameraManager.gameObject.transform.localPosition = new Vector3(0, cameraManager.YOffset, Mathf.Sqrt(Mathf.Pow(room.HalfExtent.z * Mathf.Sin(cameraManager.ObjectiveRotation * Mathf.Deg2Rad),2) + Mathf.Pow(room.HalfExtent.x * Mathf.Cos(cameraManager.ObjectiveRotation * Mathf.Deg2Rad), 2)));
    }

    public void EnterInteractState()
    {
        cameraManager.MyStateMachine.ChangeState(cameraManager.MyStateMachine.InteractState);
    }
    public void OnRotate(float direction)
    {
        rotationDirection = direction;
    }
}
