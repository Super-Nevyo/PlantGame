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
        EventManager.EnterInteractStateEvent += ExitInteract;
    }
    public void Exit()
    {
        Debug.Log("Exit InteractState");
        EventManager.EnterInteractStateEvent -= ExitInteract;
        GameManager.instance.DeslectInteractionTarget();
    }
    public void Update()
    {

    }
    public void ExitInteract()
    {
        EventManager.ExitInteract();
    }
}
