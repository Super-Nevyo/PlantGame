[System.Serializable]
public class Nutrient
{
    public string Name;
    public float AbsorptionRatePercent, AbsorptionRateMin, AmountInPlant, AmountConsumed, MinSick, MaxSick;
    public bool CheckNutrient()
    {
        return false;
    }
    public float AbsorbNutrient(float amount)
    {
        if (amount > AbsorptionRateMin)
        {
            AmountInPlant += AbsorptionRateMin + (amount - AbsorptionRateMin) * AbsorptionRatePercent / 100;
            return AbsorptionRateMin + (amount - AbsorptionRateMin) * AbsorptionRatePercent / 100;
        }
        else
        {
            AmountInPlant += amount;
            return amount;
        }
    }
}
