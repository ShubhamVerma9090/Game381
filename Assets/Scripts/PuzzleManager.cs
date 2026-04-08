//using UnityEngine;

//public class PuzzleManager : MonoBehaviour
//{
//    public static PuzzleManager Instance;

//    [Header("References")]
//    public GameObject wirePuzzleUI;

//    [Header("Streetlight Settings")]
//    public UnityEngine.Rendering.Universal.Light2D streetLight;

//    private Breaker _currentBreaker;
//    private LaptopInteraction _currentLaptop;

//    private void Awake()
//    {
//        // Setup Singleton
//        if (Instance == null) Instance = this;
//        else { Destroy(gameObject); return; }

//        // Hide puzzle on start
//        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(false);
//    }

//    public void OpenPuzzle(Breaker sender)
//    {
//        _currentBreaker = sender;
//        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(true);
//    }

//    public void ClosePuzzle()
//    {
//        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(false);
//    }

//    //public void PuzzleSolved()
//    //{
//    //    // This turns on the light regardless of which puzzle (wires, laptop, or switch) called it
//    //    if (streetLight != null)
//    //    {
//    //        streetLight.enabled = true;
//    //        Debug.Log("Global Puzzle Logic: Light Activated!");
//    //    }
//    //}

//    public void PuzzleSolved()
//    {
//        if (_currentBreaker != null)
//        {
//            _currentBreaker.FinishPuzzle();
//        }

//        // If the solved puzzle was the Laptop
//        if (_currentLaptop != null)
//        {
//            // We can call a method on the laptop or just turn the light on directly
//            // For now, let's assume the Laptop script handles its own 'Finish' logic
//            Debug.Log("Laptop Puzzle Solved via Manager!");
//        }

//        ClosePuzzle();

using UnityEngine;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [Header("UI Canvas References")]
    public GameObject wirePuzzleUI;
    public GameObject laptopUI; // This is the Canvas/Panel for the UI

    [Header("Central Power Grid")]
    [Tooltip("ONLY put lights or objects here that should stay OFF until the puzzle is solved.")]
    public List<GameObject> objectsToActivate = new List<GameObject>();

    private Breaker _currentBreaker;
    private LaptopInteraction _currentLaptop;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        // Hide UI elements only
        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(false);
        if (laptopUI != null) laptopUI.SetActive(false);

        // ONLY hide the environmental lights/objects
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null) obj.SetActive(false);
        }
    }

    public void OpenPuzzle(Breaker sender)
    {
        _currentBreaker = sender;
        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(true);
    }

    // Now correctly opens the UI without affecting the world sprite
    public void OpenLaptop(LaptopInteraction sender)
    {
        _currentLaptop = sender;
        if (laptopUI != null) laptopUI.SetActive(true);
    }

    public void ClosePuzzle()
    {
        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(false);
        if (laptopUI != null) laptopUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void PuzzleSolved()
    {
        Debug.Log("Puzzle System: Power Restored!");

        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null) obj.SetActive(true);
        }

        if (_currentBreaker != null) _currentBreaker.FinishPuzzle();
        // If the laptop is the one that solved it, we close it
        ClosePuzzle();
    }
}

//    }
//}





