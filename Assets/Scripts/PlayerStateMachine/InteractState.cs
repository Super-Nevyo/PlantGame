using UnityEngine;

public class InteractState : IState
{
    private PlayerController _player;

    public InteractState(PlayerController player)
    {
        this._player = player;
    }

    public void Enter()
    {
        Debug.Log("Enter InteractState");
        EventManager.InteractAction += ExitInteract;
    }
    public void Exit()
    {
        EventManager.InteractAction -= ExitInteract;
        GameManager.instance.DeslectInteractionTarget();
    }
    public void Update()
    {

    }
    public void ExitInteract()
    {
        EventManager.ExitInteract();
        _player.MyStateMachine.ChangeState(_player.MyStateMachine.walkState);
    }
}
