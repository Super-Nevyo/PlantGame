using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public IInteractable InteractionTarget { get; private set; }
    public Pot SelectedPot { get; private set; }

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
