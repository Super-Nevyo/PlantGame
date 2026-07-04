using UnityEngine;

[System.Serializable]
public class Nutrient
{
    public string Name;
    public float AbsorptionRatePercent, AbsorptionRateMin, AmountInPlant, AmountConsumed, MinSick, MaxSick;
    public bool CheckNutrient()
    {
        return false;
    }
    public void AbsorbNutrient(float amount)
    {
        AmountInPlant += amount;
    }
}
