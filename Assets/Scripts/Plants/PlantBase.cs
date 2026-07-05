using UnityEngine;

public class PlantBase : MonoBehaviour
{
    [SerializeField] protected PlantNutrient[] nutrients;
    private Pot _pot;
    public SpriteRenderer Sprite;


    private void Start()
    {
        foreach (var n in nutrients)
        {
            n.Initialization(this);
        }
    }
    public void DoNightCycle()
    {
        foreach (var n in nutrients)
        {
            _pot.PullNutrient(n.Name, n.AbsorbNutrient(_pot.PullNutrient(n.Name)));
            if (n.CheckNutrient())
            {
                // plant is not sick
                if (n.IsSick) n.MakePlantHealthy(); // if plant was sick, make it healthy
            }
            else
            {
                // Plant is sick
                if (!n.IsSick) n.MakePlantSick(); // if plant was healthy, make it sick
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
