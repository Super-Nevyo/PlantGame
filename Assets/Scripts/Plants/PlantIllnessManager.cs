using UnityEngine;

public class PlantIllnessManager : MonoBehaviour 
{
    public static PlantIllnessManager instance;
    private void Start()
    {
        instance = this;
    }
        public void PlantIsSick(PlantBase plant, NutrientInformation info, IsNutrientMakingPlantSick SickStatus)
    {
        // Do something when the plant is sick
        if (info.ChangeColour) { PlantIllnessManager.instance.ChangePlantColour(plant, true, info.NutrientColour); }
        if (info.ChangeSaturation) { PlantIllnessManager.instance.ChangePlantSaturation(plant, true, info.Saturation); }
        if (info.LoseGrowthStage) { PlantIllnessManager.instance.PlantLoseGrowthStage(plant, true); }
        if (info.LeafHoles) { PlantIllnessManager.instance.AddLeafHoles(plant, true); }
        if (info.InstantDeath) { PlantIllnessManager.instance.KillThePlant(plant); }
    }
    public void PlantIsHealthy(PlantBase plant, NutrientInformation info)
    {
        if (info.ChangeColour) { PlantIllnessManager.instance.ChangePlantColour(plant, false, info.NutrientColour); }
        if (info.ChangeSaturation) { PlantIllnessManager.instance.ChangePlantSaturation(plant, false, info.Saturation); }
        if (info.LoseGrowthStage) { PlantIllnessManager.instance.PlantLoseGrowthStage(plant, false); }
        if (info.LeafHoles) { PlantIllnessManager.instance.AddLeafHoles(plant, false); }
    }
    private void ChangePlantColour(PlantBase plant, bool Sick, Color NutrientColour)
    {
        // change the plant's colour
        if (Sick)
        {
            plant.Sprite.color -= NutrientColour;

        }
        else
        {
            plant.Sprite.color += NutrientColour;
        }
    }
    private void ChangePlantSaturation(PlantBase plant, bool Sick, Color Saturation)
    {
        // change the plant's saturation
        if (Sick)
        {
            plant.Sprite.color -= Saturation;

        }
        else
        {
            plant.Sprite.color += Saturation;
        }
    }
    private void PlantLoseGrowthStage(PlantBase plant, bool Sick)
    {
        // make the plant lose a growth stage
    }
    private void AddLeafHoles(PlantBase plant, bool Sick)
    {
        // add holes to the plant's leaves
    }
    private void KillThePlant(PlantBase plant)
    {
        // kill the plant
    }
}
