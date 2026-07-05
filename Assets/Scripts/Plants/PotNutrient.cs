using UnityEngine;
[System.Serializable]
public class PotNutrient
{
    public string Name;
    public float AmountInPot;

    public PotNutrient(string name, float amountInPot)
    {
        Name = name;
        AmountInPot = amountInPot;
    }
}
