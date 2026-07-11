using System;
using UnityEngine;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

public class Pot : MonoBehaviour
{
    public PotNutrient[] Nutrients;

    public float PullNutrient(NutrientInformation info)
    {
        Debug.Log("Pulling Nutrient: " + info.name);
        PotNutrient nutrient = Array.Find(Nutrients, x => x.Name == info.Name);
        if (nutrient == null)
        {
            nutrient = new PotNutrient(info, 0);
            PotNutrient[] newNutrients = new PotNutrient[Nutrients.Length + 1];
            for(int i = 0; i < Nutrients.Length; i ++)
            {
                newNutrients[i] = Nutrients[i];
            }
            newNutrients[Nutrients.Length] = nutrient;
            Nutrients = newNutrients;
            return 0;
        }
        else
        {
            return nutrient.AmountInPot;
        }
    }
    public void PullNutrient(NutrientInformation info, float amount)
    {
        Debug.Log(amount);
        PotNutrient n = Array.Find(Nutrients, x => x.Name == info.Name);
        n.AmountInPot -= amount;
    }

    public void AddNutrient(PotNutrient[] nutrients) 
    {
        foreach(var n in nutrients)
        {
            PotNutrient nutrient = Array.Find(Nutrients, x => x.Name == n.Name);
            if (nutrient == null)
            {
                nutrient = new PotNutrient(n.NutrientInformation, 0);
                PotNutrient[] newNutrients = new PotNutrient[Nutrients.Length + 1];
                for (int i = 0; i < Nutrients.Length; i++)
                {
                    newNutrients[i] = Nutrients[i];
                }
                newNutrients[Nutrients.Length] = nutrient;
                Nutrients = newNutrients;
            }
            nutrient.AmountInPot += n.AmountInPot;
            n.AmountInPot = 0;
        }
    }

}
