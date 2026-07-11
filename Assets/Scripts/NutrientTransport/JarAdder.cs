using TMPro;
using UnityEngine;

public class JarAdder : MonoBehaviour
{
    [SerializeField] private TMP_Text _addText;
    [SerializeField] private Jar jar;
    [SerializeField] private NutrientInformation[] nutrients;
    private NutrientInformation _selectedNutrient;
    private int _selectedNutrientIndex = 0;

    public void AddToJar()
    {
        Debug.Log("pourNutrients");
        jar.AddNutrient(new PotNutrient(_selectedNutrient, 20));
    }
    void Start()
    {
        ChangeNutrientTo(nutrients[0]);
    }
    void ChangeNutrientTo(NutrientInformation nutrient)
    {
        _selectedNutrient = nutrient;
        _addText.text = "Add 20 " + nutrient.Name;
    }
    public void NextNutrient()
    {
        _selectedNutrientIndex++;
        if (_selectedNutrientIndex >= nutrients.Length)
        {
            _selectedNutrientIndex = 0;
        }
        ChangeNutrientTo(nutrients[_selectedNutrientIndex]);
    }
    public void previousNutrient()
    {
        _selectedNutrientIndex--;
        if (_selectedNutrientIndex < 0)
        {
            _selectedNutrientIndex = nutrients.Length - 1;
        }
        ChangeNutrientTo(nutrients[_selectedNutrientIndex]);
    }
}
