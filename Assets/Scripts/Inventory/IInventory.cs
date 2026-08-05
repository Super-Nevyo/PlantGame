using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    public void OnClick();
    public void OnHover();
}
