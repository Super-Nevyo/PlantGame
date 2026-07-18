using UnityEngine;

public class CSInteract : IState
{
    private CameraManager cameraManager;
    private float _camDistance;
    private Vector3 _pivotLocation;
    private Quaternion _pivotRotation;
    private bool _repositioned;
    public CSInteract(CameraManager cam)
    {
        cameraManager = cam;
    }
    public void Enter()
    {
        EventManager.ExitInteractStateEvent += ExitInteract;
        //_pivotLocation = GameManager.instance.InteractionTarget.transform.position;
        (_camDistance, _pivotRotation, _pivotLocation) = GameManager.instance.InteractionTarget.GetCamLocation();
    }

    public void Exit()
    {
        EventManager.ExitInteractStateEvent -= ExitInteract;
    }

    public void Update()
    {
        if (!_repositioned)
        {
            cameraManager.CameraPivot.transform.position = Vector3.Lerp(cameraManager.CameraPivot.transform.position, _pivotLocation, 0.2f);
            cameraManager.CameraPivot.transform.rotation = Quaternion.Slerp(cameraManager.CameraPivot.transform.rotation, _pivotRotation, 0.2f);
            cameraManager.transform.localPosition = new Vector3(0, cameraManager.YOffset, _camDistance);
            if ((cameraManager.CameraPivot.transform.position - _pivotLocation).sqrMagnitude < 0.1)
            {
                Debug.Log("PivotLocked");
                cameraManager.CameraPivot.transform.position = _pivotLocation;
                cameraManager.CameraPivot.transform.rotation = _pivotRotation;
                _repositioned = true;
            }
        }
    }
    public void ExitInteract()
    {
        cameraManager.MyStateMachine.ChangeState(cameraManager.MyStateMachine.RoomsState);
    }
}
