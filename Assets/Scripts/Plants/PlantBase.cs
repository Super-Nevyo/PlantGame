using System.Collections.Generic;
using UnityEngine;

public class PlantBase : MonoBehaviour
{
    [SerializeField] protected PlantNutrient[] nutrients;
    private Pot _pot;
    public SpriteRenderer Sprite;
    private List<int> _plantWounds = new List<int>();
    private int _nutrientYes;
    private int _daysHealthy;
    [SerializeField] private int _daysHealthyToGrow;
    [SerializeField] private int _daysHealthyToFlower;
    private bool _hasNutrientsToGrow;
    public int GrowthStage { get; private set; } = 0;
    [SerializeField] private int numOfGrowthStages;


    private void Start()
    {
        foreach (var n in nutrients)
        {
            n.Initialization(this);
        }
        GameManager.instance.DayNight += DoNightCycle;
        _pot = GetComponentInParent<Pot>();
    }
    public void DoNightCycle()
    {
        Debug.Log("NightCycle Happened");
        HealWounds(); // plants need the whole night to heal wounds so wont use the absorbed nutrients, just the ones that were already in the plant
        // do all nutrient checks
        _nutrientYes = 0;
        foreach (var n in nutrients)
        {
            _pot.PullNutrient(n.NutrientInfo, n.AbsorbNutrient(_pot.PullNutrient(n.NutrientInfo)));
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
            if(n.CheckIfPlantCanGrow(GrowthStage))
            {
                _nutrientYes++;
            }
        }
        if (_nutrientYes == nutrients.Length)
            _hasNutrientsToGrow = true;
        else
            _hasNutrientsToGrow = false;
        
        if (IsReadyToGrow())
        {
            GrowPlant();
        }
        if (IsReadyToFlower())
        {
            FlowerPlant();
        }
    }
    protected void GrowPlant()
    {
        foreach (var n in nutrients)
        {
            n.ConsumeNutrientsForGrowth(GrowthStage);
        }
    }
    protected void FlowerPlant()
    {

    }
    
    protected virtual bool IsReadyToGrow()
    {
        if (_daysHealthy >= _daysHealthyToGrow && _hasNutrientsToGrow && numOfGrowthStages < GrowthStage && SpecialGrowthConditions())
        {
            ResetHealthyDays();
            return true;
        }
        else
        return false;
    }
    protected virtual bool SpecialGrowthConditions()
    {
        return true;
    }
    protected virtual bool IsReadyToFlower()
    {
        if(_daysHealthy >= _daysHealthyToFlower)
        {
            ResetHealthyDays();
            return true;
        }
        else
        return false;
    }
    public void WriteStats()
    {
        Debug.Log("Plant:");
        foreach (var n in nutrients)
        {
            Debug.Log(n.Name + ": " + n.AmountInPlant);
        }
        Debug.Log("Pot:");
        foreach(var n in _pot.Nutrients)
        {
            Debug.Log(n.Name + ": " + n.AmountInPot);
        }
    }
    private void Wounded(int level)
    {
        _plantWounds.Add(level);
    }
    private void HealWounds()
    {
        for (int i = 0; i < _plantWounds.Count; i++)
        {
            _nutrientYes = 0;
            foreach (var n in nutrients)
            {
                if (n.CheckIfWoundCanBeHealed(_plantWounds[i]))
                {
                    _nutrientYes++;
                }
            }
            if (_nutrientYes == nutrients.Length)
            {
                _plantWounds[i]--;
            }
            else
            {
                return;
            }

            if (_plantWounds[i] <= 0)
            {
                _plantWounds.RemoveAt(i);
            }
        }
    }
    public void ResetHealthyDays()
    {
        _daysHealthy = 0;
    }
}
