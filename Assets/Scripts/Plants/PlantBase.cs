using UnityEngine;

public class PlantBase : MonoBehaviour
{
    [SerializeField] protected Nutrient[] nutrients;



    public void DoNightCycle()
    {
        foreach (var n in nutrients)
        {
            if (n.CheckNutrient())
            {
                // plant is not sick
            }
            else
            {
                // Plant is sick
            }

        }
        if (IsReadyToGrow())
        {
            GrowPlant();
        }
        if (IsReadyToFlower())
        {
            //FlowerPlant();
        }
    }
    public void GrowPlant()
    {

    }
    public void WaterPlant()
    {

    }
    public void FertalizePlant(Nutrient[] InNutrients)
    {

    }
    public bool IsReadyToGrow()
    {
        return false;
    }
    public bool IsReadyToFlower()
    {
        return false;
    }
}
