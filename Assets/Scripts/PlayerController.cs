using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody RB;
    public PlayerStateMachine MyStateMachine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MyStateMachine = new PlayerStateMachine(this);
        MyStateMachine.Initialize(MyStateMachine.idleState);
        RB = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MyStateMachine.Execute();
    }
}
