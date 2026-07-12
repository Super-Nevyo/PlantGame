using UnityEngine;

public class ColourSource
{
    public string Name;
    public bool Add;
    public Vector3 Colour;
    public float Amount;
    public ColourSource(string name, bool add, Vector3 colour, float amount)
    {
        Name = name;
        Add = add;
        Colour = colour;
        Amount = amount;
    }
    public void UpdateColour(bool add, Vector3 colour, float amount)
    {
        Add = add;
        Colour = colour;
        Amount = amount;
    }
}
