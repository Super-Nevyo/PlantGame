using System;
using UnityEngine;

public class Jar : MonoBehaviour
{
    private PotNutrient[] _nutrients = new PotNutrient[0];
    private Vector4 _colour;
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        RemoveColour();
    }
    public void AddNutrient(PotNutrient nutrient)
    {
        Debug.Log("nutrient added to jar: " + nutrient.Name);
        Debug.Log(_colour);
        PotNutrient n = Array.Find(_nutrients, x => x.Name == nutrient.Name);
        if (n == null)
        {
            n = new PotNutrient(nutrient.NutrientInformation, 0);
            PotNutrient[] newNutrients = new PotNutrient[_nutrients.Length + 1];
            for (int i = 0; i < _nutrients.Length; i++)
            {
                newNutrients[i] = _nutrients[i];
            }
            newNutrients[_nutrients.Length] = n;
            _nutrients = newNutrients;
        }
        n.AmountInPot += nutrient.AmountInPot;
        ChangeColour(nutrient.NutrientInformation.NutrientColour, nutrient.AmountInPot);
    }
    public void PourNutrients(Pot pot)
    {
        if (pot == null || _nutrients == null) {Debug.Log("PourFailed"); return; }
        pot.AddNutrient(_nutrients);
        RemoveColour();
    }

    private void ChangeColour(Vector4 addColour, float amount)
    {
        _colour += amount / 2550 * addColour;
        _spriteRenderer.color = _colour;
    }
    private void RemoveColour()
    {
        _colour = new Vector4(0, 0, 0, 1);
        _spriteRenderer.color = _colour;
    }
}
