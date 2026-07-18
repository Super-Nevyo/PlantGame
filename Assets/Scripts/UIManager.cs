using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject plantCanvas;
    [SerializeField] private TMP_Text plantStats;

    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject loseCanvas;

    void OnEnable()
    {
        EventManager.EnterInteractStateEvent += EnablePlantCanvas;
        EventManager.ExitInteractStateEvent += DisablePlantCanvas;
    }
    void OnDisable()
    {
        EventManager.EnterInteractStateEvent -= EnablePlantCanvas;
        EventManager.ExitInteractStateEvent -= DisablePlantCanvas;
    }

    public void EnablePlantCanvas()
    {
        plantCanvas.SetActive(true);
    }   
    public void DisablePlantCanvas()
    {
        plantCanvas.SetActive(false);
    }

    public void DisplayPlantStats(string msg)
    {
        plantStats.SetText(msg);
    }
    public void LoseGame()
    {
        loseCanvas.SetActive(true);
    }
    public void WinGame()
    {
        winCanvas.SetActive(true);
    }
    public void ResetGame()
    {
        SceneManager.LoadScene("Programming");
    }
}
