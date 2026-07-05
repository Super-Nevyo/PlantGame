using UnityEngine;

public class NightButton : MonoBehaviour
{
    public void OnPress()
    {
        GameManager.instance.DoDayNight();
    }
}
