using UnityEngine;

public interface IInteractable
{
    public void OnInteract();
    public (float, Quaternion, Vector3) GetCamLocation();
}
