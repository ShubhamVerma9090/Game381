//using UnityEngine;

//public class SwitchManager : MonoBehaviour
//{
//    [Header("Settings")]
//    public int totalSlots = 16;

//    [Header("References")]
//    // Changed from Light2D to GameObject so you can drag the "Child" container here
//    public GameObject lightContainer;
//    public SwitchBoardInteract interactor;

//    private int greenCount = 0;

//    public void ReportState(bool isNowGreen)
//    {
//        if (isNowGreen) greenCount++;

//        if (greenCount >= totalSlots)
//        {
//            CompletePuzzle();
//        }
//    }

//    private void CompletePuzzle()
//    {
//        // 1. Turn on the container holding all the lights
//        if (lightContainer != null)
//        {
//            lightContainer.SetActive(true);
//            Debug.Log("Streetlight Activated!");
//        }

//        // 2. Close the UI
//        if (interactor != null)
//        {
//            interactor.CloseUI();
//        }
//    }
//}   

using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [Header("Settings")]
    public int totalSlots = 16;

    [Header("References")]
    // We no longer strictly need lightContainer here because PuzzleManager has the list
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
        // 1. Tell the PuzzleManager that the switchboard is solved
        // This will activate all lights in the "Switchboard Objects To Activate" list
        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.SwitchboardSolved();
            Debug.Log("Switchboard Solved! Notifying PuzzleManager.");
        }

        // 2. Close the UI (You can keep this here or move it to PuzzleManager.ClosePuzzle)
        if (interactor != null)
        {
            interactor.CloseUI();
        }
    }
}