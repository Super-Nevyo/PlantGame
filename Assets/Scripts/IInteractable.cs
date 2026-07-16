using UnityEngine;

public interface IInteractable
{
    public void OnInteract();
    public (float, Quaternion) GetCamLocation();
}
