using UnityEngine;

public class PlantBase : MonoBehaviour
{
    [SerializeField] protected Nutrient[] nutrients;
    private Pot _pot;



    public void DoNightCycle()
    {
        foreach (var n in nutrients)
        {
            _pot.PullNutrient(n.Name, n.AbsorbNutrient(_pot.PullNutrient(n.Name)));
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
    
    public bool IsReadyToGrow()
    {
        return false;
    }
    public bool IsReadyToFlower()
    {
        return false;
    }
}
