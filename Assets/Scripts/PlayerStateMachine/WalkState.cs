using UnityEngine;

public class WalkState : IState
{
    private PlayerController _player;

    public WalkState(PlayerController player)
    {
        this._player = player;
    }

    public void Enter()
    {
        Debug.Log("Enter WalkState");
    }
    public void Exit()
    {
        Debug.Log("Exit WalkState");
    }
    public void Update()
    {

    }
}
