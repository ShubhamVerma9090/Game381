using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    [Header("References")]
    public InventoryManager inventoryManager;
    public InteractPrompt interactPrompt;

    [Header("Interaction Key")]
    public KeyCode interactKey = KeyCode.E;

    private bool playerInRange = false;

    void Start()
    {
        if (interactPrompt != null)
        {
            interactPrompt.HidePrompt();
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            if (inventoryManager != null)
            {
                inventoryManager.OpenMenu();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (interactPrompt != null)
            {
                interactPrompt.ShowPrompt();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactPrompt != null)
            {
                interactPrompt.HidePrompt();
            }
        }
    }
}