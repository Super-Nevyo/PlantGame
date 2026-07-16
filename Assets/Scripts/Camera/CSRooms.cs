using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

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
        if (room != cameraManager.CurrentRoom)
        {
            room = cameraManager.CurrentRoom;
            cameraManager.ObjectiveRotation = room.CamDefaultRotation;
        }
    }

    public void Exit()
    {
        OnRotate(0);
    }

    public void Update()
    {
        cameraManager.ObjectiveRotation += rotationDirection * cameraManager.RotationSpeed * Time.fixedDeltaTime;
        if (cameraManager.ObjectiveRotation < 0) cameraManager.ObjectiveRotation += 360;
        if (cameraManager.ObjectiveRotation > 360) cameraManager.ObjectiveRotation -= 360;
        if (!CamPivotMoved)
        {
            cameraManager.CameraPivot.transform.position = Vector3.Lerp(cameraManager.transform.position, room.RoomCenter, 0.5f);
            if ((cameraManager.CameraPivot.transform.position - room.RoomCenter).sqrMagnitude < 0.1)
            {
                CamPivotMoved=true; 
            }
        }
        cameraManager.CameraPivot.transform.rotation = Quaternion.Slerp(cameraManager.CameraPivot.transform.rotation, Quaternion.Euler(0,cameraManager.ObjectiveRotation,0), 0.5f);
    }

    public void EnterInteractState()
    {

    }
    public void OnRotate(float direction)
    {
        rotationDirection = direction;
    }
}
