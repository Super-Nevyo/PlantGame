using UnityEngine;

public class PlayerStateMachine
{
    private PlayerController _player;
    public IState currentState { get; private set; }

    public IdleState idleState;
    public WalkState walkState;


    public PlayerStateMachine(PlayerController player)
    {
        _player = player;
        idleState = new IdleState(player);
        walkState = new WalkState(player);
    }

    public void Initialize(IState state)
    {
        currentState = state;
        state.Enter();
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
