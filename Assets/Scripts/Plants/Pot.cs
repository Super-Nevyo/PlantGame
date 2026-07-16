using System;
using UnityEngine;

public class Pot : MonoBehaviour, IInteractable
{
    public PotNutrient[] Nutrients;
    [SerializeField] private float camDistance;
    [SerializeField] private Quaternion camRotation;

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

    public void OnInteract()
    {
        Debug.Log("interacted with a pot");
        GameManager.instance.SelectInteractionTarget(this, this);
        EventManager.InteractionHappened();
    }
    public (float, Quaternion) GetCamLocation()
    {
        return (camDistance, camRotation);
    }

}
