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
    }
    public void Exit()
    {
        Debug.Log("Exit InteractState");
    }
    public void Update()
    {

    }
}
