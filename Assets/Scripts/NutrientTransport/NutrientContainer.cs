using System;
public class NutrientContainer
{
    public PotNutrient[] Nutrients = new PotNutrient[0];

    public void AddNutrient(PotNutrient nutrient)
    {
        PotNutrient n = Array.Find(Nutrients, x => x.Name == nutrient.Name);
        if (n == null)
        {
            n = new PotNutrient(nutrient.NutrientInformation, 0);
            PotNutrient[] newNutrients = new PotNutrient[Nutrients.Length + 1];
            for (int i = 0; i < Nutrients.Length; i++)
            {
                newNutrients[i] = Nutrients[i];
            }
            newNutrients[Nutrients.Length] = n;
            Nutrients = newNutrients;
        }
        n.AmountInPot += nutrient.AmountInPot;
    }

    public PotNutrient[] EmptyNutrients()
    {
        PotNutrient[] nutrients = Nutrients;
        Nutrients = new PotNutrient[0];
        return nutrients;
    }
}
