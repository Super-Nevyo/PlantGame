using UnityEngine;

[CreateAssetMenu(fileName = "NutrientInformation", menuName = "Scriptable Objects/NutrientInformation")]
public class NutrientInformation : ScriptableObject
{
    public bool ChangeColour, ChangeSaturation, LoseGrowthStage, LeafHoles, InstantDeath, PreventFlowering;
    public Color NutrientColour;
    public Color Saturation;

    public void PlantIsSick(PlantBase plant)
    {
        // Do something when the plant is sick
        if (ChangeColour) { ChangePlantColour(plant, true); }
        if (ChangeSaturation) { ChangePlantSaturation(plant, true); }
        if (LoseGrowthStage) { PlantLoseGrowthStage(plant, true); }
        if (LeafHoles) { AddLeafHoles(plant, true); }
        if (InstantDeath) { KillThePlant(plant); }
        if (PreventFlowering) { PreventThePlantFromFlowering(plant, true); }
    }
    public void PlantIsHealthy(PlantBase plant)
    {
        if (ChangeColour) { ChangePlantColour(plant, false); }
        if (ChangeSaturation) { ChangePlantSaturation(plant, false); }
        if (LoseGrowthStage) { PlantLoseGrowthStage(plant, false); }
        if (LeafHoles) { AddLeafHoles(plant, false); }
        if (PreventFlowering) { PreventThePlantFromFlowering(plant, false); }
    }


    private void ChangePlantColour(PlantBase plant, bool Sick)
    {
        // change the plant's colour
    }
    private void ChangePlantSaturation(PlantBase plant, bool Sick)
    {
        // change the plant's saturation
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
    private void PreventThePlantFromFlowering(PlantBase plant, bool Sick)
    {
        // prevent the plant from flowering
    }

}
