using UnityEngine;
[System.Serializable]
public class PlantNutrient
{
    public string Name;
    public float AbsorptionRatePercent, AbsorptionRateMin, AmountInPlant, AmountConsumed, MinSick, MaxSick;
    [HideInInspector] public bool IsSick = false;
    public NutrientInformation NutrientInfo;
    private PlantBase plant;

    public void Initialization(PlantBase plant)
    {
        this.plant = plant;
    }
    public bool CheckNutrient()
    {
        if (AmountInPlant < AmountConsumed)
        {
            AmountInPlant = 0;
        }
        else
        {
            AmountInPlant -= AmountConsumed;
        }
        if (AmountInPlant < MinSick || AmountInPlant > MaxSick)
        {
            return false;
        }
        else
        {
            return true;
        }
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
    public void MakePlantSick()
    {
        NutrientInfo.PlantIsSick(plant);
        IsSick = true;
    }
    public void MakePlantHealthy()
    {
        NutrientInfo.PlantIsHealthy(plant);
        IsSick = false;
    }
}
