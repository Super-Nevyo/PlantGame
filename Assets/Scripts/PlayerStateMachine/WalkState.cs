using UnityEngine;

public class WalkState : IState
{
    private PlayerController _player;
    private Vector2 _walkDirection;
    private Vector2 _actualDirection;
    private IInteractable _interactable;
    private RaycastHit _interactionHit;

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
        if (Physics.BoxCast(2 * _player.transform.forward, new Vector3(2, 2, 2), _player.transform.forward, out _interactionHit)) { 
        _interactable = _interactionHit.collider.GetComponent<IInteractable>();
        if (_interactable != null) { _interactable.OnInteract(); Debug.Log("Interacted"); }
        }
    }
    
}
