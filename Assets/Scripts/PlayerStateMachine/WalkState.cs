using Unity.VisualScripting;
using UnityEngine;

public class WalkState : IState
{
    private PlayerController _player;
    private Vector2 _walkDirection;
    private Vector2 _actualDirection;

    public WalkState(PlayerController player)
    {
        this._player = player;
    }

    public void Enter()
    {
        EventManager.MoveAction += OnWalk;
        EventManager.InteractAction += Interact;
        EventManager.EnterInteractStateEvent += EnterInteract;
        Debug.Log("Enter WalkState");
    }
    public void Exit()
    {
        EventManager.MoveAction -= OnWalk;
        EventManager.InteractAction -= Interact;
        EventManager.EnterInteractStateEvent -= EnterInteract;
        OnWalk(Vector2.zero);
        Debug.Log("Exit WalkState");
    }
    public void Update()
    {
        _actualDirection = _player.Speed * _player.DetermineDirectionBasedOnCam(_walkDirection);
        _player.Velocity = new Vector3(_actualDirection.x, _player.Velocity.y, _actualDirection.y);
    }

    public void OnWalk(Vector2 direction)
    {
        _walkDirection = direction;
    }
    public void Interact()
    {
        Debug.Log("InteractAttempted");
        if(_player.DoInteractBoxCast())
        {
            Debug.Log("hit");
            _player.Interactable?.OnInteract();
        }
    }
    public void EnterInteract()
    {
        Debug.Log("Entering Interact State");
        _player.MyStateMachine.ChangeState(_player.MyStateMachine.interactState);
    }
    
    
}
