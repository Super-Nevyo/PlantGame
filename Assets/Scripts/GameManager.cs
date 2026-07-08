using UnityEngine;

public class GameManager : MonoBehaviour
{
    public System.Action DayNight;
    public static GameManager instance;
    public void Awake()
    {
        instance = this;
    }

    public void DoDayNight()
    {
        DayNight?.Invoke();
    }
}
