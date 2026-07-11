using UnityEngine;

[CreateAssetMenu(fileName = "NutrientInformation", menuName = "Scriptable Objects/NutrientInformation")]
public class NutrientInformation : ScriptableObject
{
    
    public string Name;
    [Header("Effects of Nutrient Deficiency")]
    public bool ChangeColour;
    public bool ChangeSaturation, LoseGrowthStage, LeafHoles, InstantDeath, RestrictNutrientIntake;
    [Header("Effects of Nutrient Overabundance")]
    public bool ChangeColourHigh;
    public bool ChangeSaturationHigh, LoseGrowthStageHigh, LeafHolesHigh, InstantDeathHigh, RestrictNutrientIntakeHigh;
    public Color NutrientColour;
    public Color Saturation;

    

}
