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

//    [Header("Switchboard Setup")]
//    public GameObject switchboardUI;
//    public List<GameObject> switchboardObjectsToActivate = new List<GameObject>();

//    private Breaker _currentBreaker;
//    private LaptopInteraction _currentLaptop;

//    private void Awake()
//    {
//        if (Instance == null) Instance = this;
//        else { Destroy(gameObject); return; }

//        // Hide UI elements
//        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(false);
//        if (laptopUI != null) laptopUI.SetActive(false);
//        if (switchboardUI != null) switchboardUI.SetActive(false);

//        // Hide all environmental objects on start
//        ToggleObjectList(wireObjectsToActivate, false);
//        ToggleObjectList(laptopObjectsToActivate, false);
//        ToggleObjectList(switchboardObjectsToActivate, false);
//    }

//    // --- Opening UIs ---

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

//    public void OpenSwitchboard()
//    {
//        if (switchboardUI != null) switchboardUI.SetActive(true);
//    }

//    public void ClosePuzzle()
//    {
//        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(false);
//        if (laptopUI != null) laptopUI.SetActive(false);
//        if (switchboardUI != null) switchboardUI.SetActive(false);
//        Time.timeScale = 1;
//    }

//    // --- Solving Puzzles ---

//    public void WirePuzzleSolved()
//    {
//        ToggleObjectList(wireObjectsToActivate, true);
//        if (_currentBreaker != null) _currentBreaker.FinishPuzzle();
//        ClosePuzzle();
//    }

//    public void LaptopPuzzleSolved()
//    {
//        ToggleObjectList(laptopObjectsToActivate, true);
//        ClosePuzzle();
//    }

//    public void SwitchboardSolved()
//    {
//        ToggleObjectList(switchboardObjectsToActivate, true);
//        ClosePuzzle();
//    }

//    // Helper method to reduce code repetition
//    private void ToggleObjectList(List<GameObject> list, bool state)
//    {
//        foreach (GameObject obj in list)
//        {
//            if (obj != null) obj.SetActive(state);
//        }
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

    [HideInInspector] public bool isUIOpen = false;

    private Breaker _currentBreaker;
    private LaptopInteraction _currentLaptop;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(false);
        if (laptopUI != null) laptopUI.SetActive(false);
        if (switchboardUI != null) switchboardUI.SetActive(false);

        ToggleObjectList(wireObjectsToActivate, false);
        ToggleObjectList(laptopObjectsToActivate, false);
        ToggleObjectList(switchboardObjectsToActivate, false);

        isUIOpen = false;
        Time.timeScale = 1f;
    }

    public void OpenPuzzle(Breaker sender)
    {
        _currentBreaker = sender;
        isUIOpen = true;

        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(true);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OpenLaptop(LaptopInteraction sender)
    {
        _currentLaptop = sender;
        isUIOpen = true;

        if (laptopUI != null) laptopUI.SetActive(true);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (_currentLaptop != null)
        {
            _currentLaptop.OpenLaptop();
        }
    }

    public void OpenSwitchboard()
    {
        isUIOpen = true;

        if (switchboardUI != null) switchboardUI.SetActive(true);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ClosePuzzle()
    {
        if (wirePuzzleUI != null) wirePuzzleUI.SetActive(false);
        if (laptopUI != null) laptopUI.SetActive(false);
        if (switchboardUI != null) switchboardUI.SetActive(false);

        isUIOpen = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void WirePuzzleSolved()
    {
        ToggleObjectList(wireObjectsToActivate, true);

        if (_currentBreaker != null)
            _currentBreaker.FinishPuzzle();

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

    private void ToggleObjectList(List<GameObject> list, bool state)
    {
        foreach (GameObject obj in list)
        {
            if (obj != null)
                obj.SetActive(state);
        }
    }
}