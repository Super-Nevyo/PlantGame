using System;
using UnityEngine;
[Serializable]
public struct PlantNutrientStage
{
    public float AbsorptionRatePercent, AbsorptionRateMin, AmountConsumed, MinSick, MaxSick, NeededToHealWounds, NeededToGrow;
}
[System.Serializable]
public class PlantNutrient
{
    public string Name;
    public float[] AbsorptionRatePercent, AbsorptionRateMin, AmountConsumed, MinSick, MaxSick, NeededToHealWounds, NeededToGrow;
    public PlantNutrientStage[] PlantNutrientStages;
    public float AmountInPlant;
    public bool SickCausesDeath;
    [HideInInspector] public bool IsSick = false;
    public NutrientInformation NutrientInfo;
    private PlantBase plant;
    private IsNutrientMakingPlantSick _nutrientStatus;

    public PlantNutrient()
    {
        
    }

    public void Initialization(PlantBase plant)
    {
        this.plant = plant;
        //this.AbsorptionRatePercent = this.AbsorptionRateMin = AmountConsumed = MinSick = MaxSick = NeededToHealWounds= new float[plant.NumOfGrowthStages];
    }
    public bool CheckNutrient(int Stage)
    {
        if (AmountInPlant < PlantNutrientStages[Stage].AmountConsumed)
        {
            AmountInPlant = 0;
        }
        else
        {
            AmountInPlant -= PlantNutrientStages[Stage].AmountConsumed;
        }
        if (AmountInPlant < PlantNutrientStages[Stage].MinSick)
        {
            _nutrientStatus = IsNutrientMakingPlantSick.NUTRIENT_TOO_LOW;
            return false;
        }
        else if (AmountInPlant > PlantNutrientStages[Stage].MaxSick) {
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
        if (amount > PlantNutrientStages[Stage].AbsorptionRateMin)
        {
            AmountInPlant += PlantNutrientStages[Stage].AbsorptionRateMin + (amount - PlantNutrientStages[Stage].AbsorptionRateMin) * PlantNutrientStages[Stage].AbsorptionRatePercent / 100;
            return PlantNutrientStages[Stage].AbsorptionRateMin + (amount - PlantNutrientStages[Stage].AbsorptionRateMin) * PlantNutrientStages[Stage].AbsorptionRatePercent / 100;
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
        AmountInPlant -= PlantNutrientStages[Stage].NeededToHealWounds;
    }
    public bool CheckIfPlantCanGrow(int Stage)
    {
        if (AmountInPlant >= PlantNutrientStages[Stage].NeededToGrow)
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
        AmountInPlant -= PlantNutrientStages[Stage].NeededToGrow;
    }
    public float pullSap(float fraction)
    {
        AmountInPlant -= AmountInPlant * fraction;
        return AmountInPlant / (1 - fraction) * fraction;// dont want to use a temp variable so this gives back amount * fraction because of math, its just a system of equations
    }
}
