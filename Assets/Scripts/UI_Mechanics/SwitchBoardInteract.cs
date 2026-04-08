using UnityEngine;

public class SwitchBoardInteract : MonoBehaviour
{
    [Header("References")]
    public GameObject switchboardUI; // Drag the "SwitchBoard" Canvas/Panel here

    private bool isPlayerNearby = false;

    void Update()
    {
        // Open UI when pressing E near the object
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            OpenUI();
        }
    }

    public void OpenUI()
    {
        switchboardUI.SetActive(true);
        Time.timeScale = 0; // Pause game for the puzzle
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseUI()
    {
        switchboardUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) isPlayerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) isPlayerNearby = false;
    }
}