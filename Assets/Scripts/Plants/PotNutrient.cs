using UnityEngine;
[System.Serializable]
public class PotNutrient
{
    public string Name;
    public float AmountInPot;
    public NutrientInformation NutrientInformation;

    public PotNutrient(NutrientInformation connectedNutrient, float amountInPot)
    {
        Name = connectedNutrient.Name;
        AmountInPot = amountInPot;
        NutrientInformation = connectedNutrient;
    }
}
