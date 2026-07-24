using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public Rigidbody RB;
    public PlayerStateMachine MyStateMachine;
    [SerializeField] private Transform _camLocation;
    public Vector3 Velocity;
    [SerializeField] public float Speed;
    [Header("InteractionBoxCast")]
    [SerializeField] private float boxStartDistance;
    [SerializeField] private Vector3 halfExtent;
    [SerializeField] private float maxDistance;
    public RaycastHit RayHit;
    private bool _didHit;
    public IInteractable Interactable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MyStateMachine = new PlayerStateMachine(this);
        MyStateMachine.Initialize(MyStateMachine.idleState);
        RB = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        if (MyStateMachine != null && MyStateMachine.currentState == null)
        {
            MyStateMachine.Initialize(MyStateMachine.idleState);
        }
    }
    void OnDisable()
    {
        MyStateMachine.Disable();
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

    public bool DoInteractBoxCast()
    {
        // raycasting is weird, not sure if i fully understand
        _didHit = (Physics.BoxCast(boxStartDistance * transform.forward + transform.position, halfExtent, transform.forward, out RayHit, transform.rotation, maxDistance,LayerMask.GetMask("Interactable")));
        Debug.Log(RayHit.collider);
        Interactable = RayHit.collider?.GetComponent<IInteractable>();
        return _didHit;
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(boxStartDistance * transform.forward + transform.position, 2 * halfExtent);
        Gizmos.DrawWireCube((boxStartDistance + maxDistance) * transform.forward + transform.position, 2 * halfExtent);
    }
#endif
}
