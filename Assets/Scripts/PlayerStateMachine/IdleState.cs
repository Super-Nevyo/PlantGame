using UnityEngine;

public class IdleState : IState
{
    private PlayerController _player;

    public IdleState(PlayerController player)
    {
        _player = player;
    }

    public void Enter()
    {
        Debug.Log("Enter IdleState");
    }
    public void Exit()
    {
        Debug.Log("ExitIdleState");
    }
    public void Update()
    {

    }



}
