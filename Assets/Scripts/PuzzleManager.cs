//using UnityEngine;
//using System.Collections.Generic;

//public class PuzzleManager : MonoBehaviour
//{
//    public static PuzzleManager Instance;

//    [Header("Wire Puzzle Setup")]
//    public GameObject wirePuzzleUI;
//    public List<GameObject> wireObjectsToActivate = new List<GameObject>();

//    [Header("Laptop Puzzle Setup")]
//    public GameObject laptopUI;
//    public List<GameObject> laptopObjectsToActivate = new List<GameObject>();

//    private Breaker _currentBreaker;
//    private LaptopInteraction _currentLaptop;

//    private void Awake()
//    {
//        if (Instance == null) Instance = this;
//        else { Destroy(gameObject); return; }

//        // Hide UI elements
//        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(false);
//        if (laptopUI != null) laptopUI.SetActive(false);

//        // Hide all environmental objects on start
//        foreach (GameObject obj in wireObjectsToActivate) if (obj != null) obj.SetActive(false);
//        foreach (GameObject obj in laptopObjectsToActivate) if (obj != null) obj.SetActive(false);
//    }

//    public void OpenPuzzle(Breaker sender)
//    {
//        _currentBreaker = sender;
//        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(true);
//    }

//    public void OpenLaptop(LaptopInteraction sender)
//    {
//        _currentLaptop = sender;
//        if (laptopUI != null) laptopUI.SetActive(true);
//    }

//    public void ClosePuzzle()
//    {
//        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(false);
//        if (laptopUI != null) laptopUI.SetActive(false);
//        Time.timeScale = 1;
//    }

//    // Call this when the Wire Puzzle is finished
//    public void WirePuzzleSolved()
//    {
//        foreach (GameObject obj in wireObjectsToActivate) if (obj != null) obj.SetActive(true);
//        if (_currentBreaker != null) _currentBreaker.FinishPuzzle();
//        ClosePuzzle();
//    }

//    // Call this when the Laptop Puzzle is finished
//    public void LaptopPuzzleSolved()
//    {
//        foreach (GameObject obj in laptopObjectsToActivate) if (obj != null) obj.SetActive(true);
//        // Add laptop finish logic here if needed
//        ClosePuzzle();
//    }
//}

using UnityEngine;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [Header("Wire Puzzle Setup")]
    public GameObject wirePuzzleUI;
    public List<GameObject> wireObjectsToActivate = new List<GameObject>();

    [Header("Laptop Puzzle Setup")]
    public GameObject laptopUI;
    public List<GameObject> laptopObjectsToActivate = new List<GameObject>();

    [Header("Switchboard Setup")]
    public GameObject switchboardUI;
    public List<GameObject> switchboardObjectsToActivate = new List<GameObject>();

    private Breaker _currentBreaker;
    private LaptopInteraction _currentLaptop;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        // Hide UI elements
        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(false);
        if (laptopUI != null) laptopUI.SetActive(false);
        if (switchboardUI != null) switchboardUI.SetActive(false);

        // Hide all environmental objects on start
        ToggleObjectList(wireObjectsToActivate, false);
        ToggleObjectList(laptopObjectsToActivate, false);
        ToggleObjectList(switchboardObjectsToActivate, false);
    }

    // --- Opening UIs ---

    public void OpenPuzzle(Breaker sender)
    {
        _currentBreaker = sender;
        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(true);
    }

    public void OpenLaptop(LaptopInteraction sender)
    {
        _currentLaptop = sender;
        if (laptopUI != null) laptopUI.SetActive(true);
    }

    public void OpenSwitchboard()
    {
        if (switchboardUI != null) switchboardUI.SetActive(true);
    }

    public void ClosePuzzle()
    {
        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(false);
        if (laptopUI != null) laptopUI.SetActive(false);
        if (switchboardUI != null) switchboardUI.SetActive(false);
        Time.timeScale = 1;
    }

    // --- Solving Puzzles ---

    public void WirePuzzleSolved()
    {
        ToggleObjectList(wireObjectsToActivate, true);
        if (_currentBreaker != null) _currentBreaker.FinishPuzzle();
        ClosePuzzle();
    }

    public void LaptopPuzzleSolved()
    {
        ToggleObjectList(laptopObjectsToActivate, true);
        ClosePuzzle();
    }

    public void SwitchboardSolved()
    {
        ToggleObjectList(switchboardObjectsToActivate, true);
        ClosePuzzle();
    }

    // Helper method to reduce code repetition
    private void ToggleObjectList(List<GameObject> list, bool state)
    {
        foreach (GameObject obj in list)
        {
            if (obj != null) obj.SetActive(state);
        }
    }
}