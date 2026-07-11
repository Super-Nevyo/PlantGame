using UnityEngine;
[System.Serializable]
public class PlantNutrient
{
    public string Name;
    public float AbsorptionRatePercent, AbsorptionRateMin, AmountInPlant, AmountConsumed, MinSick, MaxSick, NeededToHealWounds, NeededToGrow;
    [HideInInspector] public bool IsSick = false;
    public NutrientInformation NutrientInfo;
    private PlantBase plant;
    private IsNutrientMakingPlantSick _nutrientStatus;

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
        if (AmountInPlant < MinSick)
        {
            _nutrientStatus = IsNutrientMakingPlantSick.NUTRIENT_TOO_LOW;
            return false;
        }
        else if (AmountInPlant > MaxSick) {
            _nutrientStatus = IsNutrientMakingPlantSick.NUTRIENT_TOO_HIGH;
            return false; 
        }
        else
        {
            _nutrientStatus = IsNutrientMakingPlantSick.NUTRIENT_OK;
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
        // illness manager(nutrient info)
        PlantIllnessManager.instance.PlantIsSick(plant, NutrientInfo, _nutrientStatus);
        IsSick = true;
    }
    public void MakePlantHealthy()
    {
        PlantIllnessManager.instance.PlantIsHealthy(plant, NutrientInfo);
        IsSick = false;
    }

    public bool CheckIfWoundCanBeHealed(int Level)
    {
        if (AmountInPlant >= NeededToHealWounds * Level)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void HealWound(int Level)
    {
        AmountInPlant -= NeededToHealWounds * Level;
    }
    public bool CheckIfPlantCanGrow(int Level)
    {
        if (AmountInPlant >= NeededToGrow * Level)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ConsumeNutrientsForGrowth(int Level)
    {
        AmountInPlant -= NeededToGrow * Level;
    }
}
