
public class CameraStateMachine
{
    private CameraManager _cameraManager;
    public CSInteract InteractState;
    public CSRooms RoomsState;
    public IState currentState;
    public CameraStateMachine(CameraManager camManager)
    {
        _cameraManager = camManager;
        InteractState = new CSInteract(camManager);
        RoomsState = new CSRooms(camManager);
    }

    public void Initialize(IState state)
    {
        currentState = state;
        state.Enter();
    }
    public void Disable()
    {
        currentState.Exit();
        currentState = null;
    }

    public void ChangeState(IState state)
    {
        currentState.Exit();
        currentState = state;
        state.Enter();
    }

    public void Execute()
    {
        currentState?.Update();
    }
}
