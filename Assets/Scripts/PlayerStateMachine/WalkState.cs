using Unity.VisualScripting;
using UnityEngine;

public class WalkState : IState
{
    private PlayerController _player;
    private Vector2 _walkDirection;
    private Vector2 _actualDirection;
    private IInteractable _interactable;
    private RaycastHit _interactionHit;
    private bool _didHit;

    public WalkState(PlayerController player)
    {
        this._player = player;
    }

    public void Enter()
    {
        InputManager.instance.MoveAction += OnWalk;
        InputManager.instance.InteractAction += Interact;
        Debug.Log("Enter WalkState");
    }
    public void Exit()
    {
        InputManager.instance.MoveAction -= OnWalk;
        InputManager.instance.InteractAction -= Interact;
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
            _player.RayHit = _interactionHit;
            Debug.Log("hit");
            // TODO: why is this causing an error
            _interactable = _player.RayHit.collider.GetComponent<IInteractable>();
            if (_interactable != null) { _interactable.OnInteract(); Debug.Log("Interacted"); }
        }
    }
    
    
}
