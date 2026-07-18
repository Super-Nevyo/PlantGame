using UnityEngine;
[System.Serializable]
public class PlantNutrient
{
    public string Name;
    public float[] AbsorptionRatePercent, AbsorptionRateMin, AmountConsumed, MinSick, MaxSick, NeededToHealWounds, NeededToGrow;
    public float AmountInPlant;
    public bool SickCausesDeath;
    [HideInInspector] public bool IsSick = false;
    public NutrientInformation NutrientInfo;
    private PlantBase plant;
    private IsNutrientMakingPlantSick _nutrientStatus;

    public void Initialization(PlantBase plant)
    {
        this.plant = plant;
    }
    public bool CheckNutrient(int Stage)
    {
        if (AmountInPlant < AmountConsumed[Stage])
        {
            AmountInPlant = 0;
        }
        else
        {
            AmountInPlant -= AmountConsumed[Stage];
        }
        if (AmountInPlant < MinSick[Stage])
        {
            _nutrientStatus = IsNutrientMakingPlantSick.NUTRIENT_TOO_LOW;
            return false;
        }
        else if (AmountInPlant > MaxSick[Stage]) {
            _nutrientStatus = IsNutrientMakingPlantSick.NUTRIENT_TOO_HIGH;
            return false; 
        }
        else
        {
            _nutrientStatus = IsNutrientMakingPlantSick.NUTRIENT_OK;
            return true;
        }
    }
    public float AbsorbNutrient(float amount, int Stage)
    {
        if (amount > AbsorptionRateMin[Stage])
        {
            AmountInPlant += AbsorptionRateMin[Stage] + (amount - AbsorptionRateMin[Stage]) * AbsorptionRatePercent[Stage] / 100;
            return AbsorptionRateMin[Stage] + (amount - AbsorptionRateMin[Stage]) * AbsorptionRatePercent[Stage] / 100;
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
        if (AmountInPlant >= NeededToHealWounds[plant.GrowthStage] * Level)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void HealWound(int Stage)
    {
        AmountInPlant -= NeededToHealWounds[Stage];
    }
    public bool CheckIfPlantCanGrow(int Stage)
    {
        if (AmountInPlant >= NeededToGrow[Stage])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ConsumeNutrientsForGrowth(int Stage)
    {
        AmountInPlant -= NeededToGrow[Stage];
    }
    public float pullSap(float fraction)
    {
        AmountInPlant -= AmountInPlant * fraction;
        return AmountInPlant / (1 - fraction) * fraction;// dont want to use a temp variable so this gives back amount * fraction because of math, its just a system of equations
    }
}
