using UnityEngine;

public class SwitchBoardInteract : MonoBehaviour
{
    [Header("References")]
    public GameObject switchboardUI; // Drag the "SwitchBoard" Canvas/Panel here
    public GameObject promptText;    // Drag your "Press E to Repair" TextMeshPro object here

    private bool isPlayerNearby = false;
    private bool isUIOpen = false;

    private void Start()
    {
        if (promptText != null)
            promptText.SetActive(false);

        if (switchboardUI != null)
            switchboardUI.SetActive(false);
    }

    void Update()
    {
        // Open UI when pressing E near the object
        if (isPlayerNearby && !isUIOpen && Input.GetKeyDown(KeyCode.E))
        {
            OpenUI();
        }
    }

    public void OpenUI()
    {
        if (switchboardUI != null)
            switchboardUI.SetActive(true);

        isUIOpen = true;

        if (promptText != null)
            promptText.SetActive(false);

        Time.timeScale = 0; // Pause game
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseUI()
    {
        if (switchboardUI != null)
            switchboardUI.SetActive(false);

        isUIOpen = false;

        // Show prompt again if player is still nearby
        if (isPlayerNearby && promptText != null)
            promptText.SetActive(true);

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;

            if (!isUIOpen && promptText != null)
                promptText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            if (promptText != null)
                promptText.SetActive(false);
        }
    }
}