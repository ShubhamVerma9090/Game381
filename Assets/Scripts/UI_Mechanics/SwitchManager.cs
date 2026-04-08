using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [Header("Settings")]
    public int totalSlots = 16;

    [Header("References")]
    // Changed from Light2D to GameObject so you can drag the "Child" container here
    public GameObject lightContainer;
    public SwitchBoardInteract interactor;

    private int greenCount = 0;

    public void ReportState(bool isNowGreen)
    {
        if (isNowGreen) greenCount++;

        if (greenCount >= totalSlots)
        {
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        // 1. Turn on the container holding all the lights
        if (lightContainer != null)
        {
            lightContainer.SetActive(true);
            Debug.Log("Streetlight Activated!");
        }

        // 2. Close the UI
        if (interactor != null)
        {
            interactor.CloseUI();
        }
    }
}   