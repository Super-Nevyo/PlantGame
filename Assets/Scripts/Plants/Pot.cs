using System;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public PotNutrient[] Nutrients;

    public float PullNutrient(string name)
    {
        PotNutrient n = Array.Find(Nutrients, x => x.Name == name);
        if (n == null)
        {
            n = new PotNutrient(name, 0);
            PotNutrient[] newNutrients = new PotNutrient[Nutrients.Length + 1];
            for(int i = 0; i < Nutrients.Length; i ++)
            {
                newNutrients[i] = Nutrients[i];
            }
            newNutrients[Nutrients.Length] = n;
            Nutrients = newNutrients;
            return 0;
        }
        else
        {
            return n.AmountInPot;
        }
    }
    public void PullNutrient(string name, float amount)
    {
        PotNutrient n = Array.Find(Nutrients, x => x.Name == name);
        n.AmountInPot -= amount;
    }

}
