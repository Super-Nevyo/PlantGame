using UnityEngine;

public class GrabbableItem : MonoBehaviour, IInventoryItem
{
    public string Name {  get; private set; }
    private bool _isGrabbed = false;
    public void OnClick()
    {
        // Handle click event
        if (!_isGrabbed)
        {
            _isGrabbed = true;
            // Add logic for when the item is grabbed
            Debug.Log($"{Name} has been grabbed.");
        }
        else
        {
            // check if there is a thing for the item to interact with, if so, do the interaction, if not, release the item
            _isGrabbed = false;
            // Add logic for when the item is released
            Debug.Log($"{Name} has been released.");
        }
    }

    public void OnHover()
    {
        // trigger glow
    }
}
