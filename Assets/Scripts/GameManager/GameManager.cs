using UnityEngine;

public class GameManager : MonoBehaviour
{
    public IInteractable InteractionTarget { get; private set; }
    public Pot SelectedPot { get; private set; }
    public static GameManager instance;
    public void Awake()
    {
        instance = this;
    }

    public void DoDayNight()
    {
        EventManager.DoNight();
    }
    public void SelectInteractionTarget(IInteractable interacteble, Pot pot)
    {
        InteractionTarget = interacteble;
        SelectedPot = pot;
    }
    public void SelectInteractionTarget(IInteractable interacteble)
    {
        InteractionTarget = interacteble;
    }
    public void DeslectInteractionTarget()
    {
        InteractionTarget = null;
    }
}
