using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private float _startPositionY;
    [SerializeField] private float MoveAmountY;
    [SerializeField] private float DoorSpeed;
    [SerializeField] private GameObject door;
    private List<Collider> _colliders;
    private bool _doorOpen;
    void Start()
    {
        _startPositionY = door.transform.position.y;
        _colliders = new List<Collider>();
        _doorOpen = false;
    }
    private void FixedUpdate()
    {
        if (_doorOpen) {
            door.transform.position = new Vector3(door.transform.position.x, Mathf.MoveTowards(door.transform.position.y, _startPositionY + MoveAmountY, DoorSpeed), door.transform.position.z);
        }
        else
        {
            door.transform.position = new Vector3(door.transform.position.x, Mathf.MoveTowards(door.transform.position.y, _startPositionY, DoorSpeed), door.transform.position.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        _colliders.Add(other);
        if (!_doorOpen)
        {
            _doorOpen = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _colliders.Remove(other);
        if (_colliders.Count == 0) {
            _doorOpen = false;
        }
    }
}
