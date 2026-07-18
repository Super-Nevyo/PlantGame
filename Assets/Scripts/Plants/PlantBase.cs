using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class PlantBase : MonoBehaviour
{
    [SerializeField] protected PlantNutrient[] nutrients;
    private Pot _pot;
    public SpriteRenderer Sprite;
    private List<int> _plantWounds = new List<int>();
    private int _totalWounds = 0;
    public int WoundsToPreventGrowth;
    public int WoundsToKill;
    private int _nutrientYes;
    private int _daysHealthy;
    private int _daysSick;
    [SerializeField] private int _daysHealthyToGrow;
    [SerializeField] private int _daysHealthyToFlower;
    private bool _hasNutrientsToGrow;
    public int GrowthStage { get; private set; } = 1;
    [SerializeField] private int numOfGrowthStages;
    [SerializeField] private Sprite[] sprites;
    private float _mlofSap = 500;
    [SerializeField] private float daysSickToDie;
    private bool _isPlantDead;

    //remove when information communication is solved
    [SerializeField] UIManager UI;


    private void Start()
    {
        foreach (var n in nutrients)
        {
            n.Initialization(this);
        }
        
        _pot = GetComponentInParent<Pot>();
        Sprite.sprite = sprites[GrowthStage];
    }
    private void OnEnable()
    {
        EventManager.NightEvent += DoNightCycle;
    }
    private void OnDisable()
    {
        EventManager.NightEvent -= DoNightCycle;
    }
    public void DoNightCycle()
    {
        if(_isPlantDead) return;
        Debug.Log("NightCycle Happened");
        if (_daysHealthy <= 0) _daysSick++;
        else _daysSick = 0;
        _daysHealthy++;
        HealWounds(); // plants need the whole night to heal wounds so wont use the absorbed nutrients, just the ones that were already in the plant
        _totalWounds = 0;
        foreach (int i in _plantWounds)
        {
            _totalWounds += i;
        }
        if (_totalWounds >= WoundsToPreventGrowth) { ResetHealthyDays(); Debug.Log("Plant Too Wounded"); }
        if (_totalWounds >= WoundsToKill) KillPlant();
        // do all nutrient checks
        _nutrientYes = 0;
        foreach (var n in nutrients)
        {
            _pot.PullNutrient(n.NutrientInfo, n.AbsorbNutrient(_pot.PullNutrient(n.NutrientInfo), GrowthStage));
            if (n.CheckNutrient(GrowthStage))
            {
                // plant is not sick
                if (n.IsSick) n.MakePlantHealthy(); // if plant was sick, make it healthy
            }
            else
            {
                // Plant is sick
                if (!n.IsSick) n.MakePlantSick(); // if plant was healthy, make it sick

                if (n.SickCausesDeath) ResetHealthyDays();
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
        if (ShouldPlantDie())
        {
            KillPlant();
        }
    }
    protected void GrowPlant()
    {
        foreach (var n in nutrients)
        {
            n.ConsumeNutrientsForGrowth(GrowthStage);
        }
        GrowthStage++;
        Sprite.sprite = sprites[GrowthStage];
    }
    protected virtual void FlowerPlant()
    {
        // TODO: make this do something
    }
    
    protected virtual bool IsReadyToGrow()
    {
        Debug.Log(_daysHealthy >= _daysHealthyToGrow);
        Debug.Log(_hasNutrientsToGrow );
        Debug.Log( numOfGrowthStages > GrowthStage);
        Debug.Log(SpecialGrowthConditions());
        if (_daysHealthy >= _daysHealthyToGrow && _hasNutrientsToGrow && numOfGrowthStages > GrowthStage && SpecialGrowthConditions())
        {
            Debug.Log("Is Ready To Grow");
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
            //ResetHealthyDays();
            return true;
        }
        else
        return false;
    }
    public void WriteStats()
    {
        if (_isPlantDead)
        {
            UI.LoseGame();
            return;
        }
        if (GrowthStage == 3)
        {
            UI.WinGame();
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("PlantStats:");
        Debug.Log("Plant:");
        foreach (var n in nutrients)
        {
            Debug.Log(n.Name + ": " + n.AmountInPlant);
            sb.AppendLine(n.Name + ": " + n.AmountInPlant);
        }
        sb.AppendLine("Pot:");
        Debug.Log("Pot:");
        foreach(var n in _pot.Nutrients)
        {
            Debug.Log(n.Name + ": " + n.AmountInPot);
            sb.AppendLine(n.Name + ": " + n.AmountInPot);
        }
        UI.DisplayPlantStats(sb.ToString());
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
    private void ResetHealthyDays()
    {
        _daysHealthy = 0;
    }
    protected virtual bool ShouldPlantDie()
    {
        if (_daysSick >= daysSickToDie) return true;
        return false;
    }
    public virtual void KillPlant()
    {
        _isPlantDead = true;
        Sprite.color = Color.black;
    }
    public (float, PotNutrient[]) PullSap(float MlSap)
    {
        PotNutrient[] givenNutrients = new PotNutrient[nutrients.Length];
        if (MlSap < _mlofSap)
        {
            Wounded(Mathf.CeilToInt(MlSap * 5 / _mlofSap));
            for (int i = 0; i < nutrients.Length; i++)
            {
                givenNutrients[i] = new PotNutrient(nutrients[i].NutrientInfo, nutrients[i].pullSap(MlSap / _mlofSap));
            }
            _mlofSap -= MlSap;
            _mlofSap = 500; // this is temp to reset the sap, will be changed when the sap is implemented in the game, this is just to test the nutrient pulling
            return (MlSap, givenNutrients);
        }
        else
        {
            Wounded(5);
            for (int i = 0; i < nutrients.Length; i++)
            {
                givenNutrients[i] = new PotNutrient(nutrients[i].NutrientInfo, nutrients[i].pullSap(1));
            }
            _mlofSap = 0;
            return (_mlofSap, givenNutrients);
        }
    }
}
