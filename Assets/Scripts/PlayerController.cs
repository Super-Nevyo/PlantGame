using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody RB;
    public PlayerStateMachine MyStateMachine;
    [SerializeField] private Transform _camLocation;
    public Vector3 Velocity;
    [SerializeField] public float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MyStateMachine = new PlayerStateMachine(this);
        MyStateMachine.Initialize(MyStateMachine.idleState);
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MyStateMachine.Execute();
    }
    void FixedUpdate()
    {
        RB.linearVelocity = Velocity;
    }

    
    public Vector2 DetermineDirectionBasedOnCam(Vector2 direction)
    {
        Vector2 LocalVector = new Vector2(_camLocation.forward.x, _camLocation.forward.z);
        return (direction.y * LocalVector + direction.x * new Vector2(LocalVector.y,-LocalVector.x)).normalized;
    }
}
