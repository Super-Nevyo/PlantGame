using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float turnSpeed;
    [SerializeField] public float jumpForce;
    private Vector2 _direction;
    private Vector2 direction;
    private Vector2 _turnDirection;
    private Rigidbody _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        _direction = direction != Vector2.zero ? new Vector2(Mathf.Cos(Mathf.Atan2(direction.y, direction.x) - _rb.rotation.eulerAngles.y * Mathf.Deg2Rad), Mathf.Sin(Mathf.Atan2(direction.y, direction.x) - _rb.rotation.eulerAngles.y * Mathf.Deg2Rad)) : Vector2.zero;
        _rb.linearVelocity = new Vector3(_direction.x * speed, _rb.linearVelocity.y, _direction.y * speed);
        _rb.angularVelocity = turnSpeed * _turnDirection.x * Vector3.up;
        Debug.Log(_rb.angularVelocity);
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
        
    }

    public void OnJump(InputValue value)
    {
        _rb.AddForce(0,jumpForce,0);
    }

    public void OnLook(InputValue value)
    {
        _turnDirection = value.Get<Vector2>();
    }
}
