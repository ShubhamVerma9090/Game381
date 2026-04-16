using UnityEngine;

public class VendingMachineInteract : MonoBehaviour
{
    [SerializeField] private GameObject vendingPanel;
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Prompt Text (TextMeshPro Object)")]
    [SerializeField] private GameObject promptText; // drag your TMP text here

    private bool playerNear = false;
    private bool menuOpen = false;

    private void Start()
    {
        if (promptText != null)
            promptText.SetActive(false);

        if (vendingPanel != null)
            vendingPanel.SetActive(false);
    }

    private void Update()
    {
        if (!playerNear) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleVendingMenu();
        }
    }

    private void ToggleVendingMenu()
    {
        if (vendingPanel == null) return;

        menuOpen = !menuOpen;
        vendingPanel.SetActive(menuOpen);

        // Hide/show text based on menu state
        if (promptText != null)
        {
            if (menuOpen)
                promptText.SetActive(false);
            else if (playerNear)
                promptText.SetActive(true);
        }
    }

    public void BuyItem(string itemName)
    {
        Debug.Log("Clicked: " + itemName);

        if (playerInventory != null)
        {
            playerInventory.AddItem(itemName);
            Debug.Log(itemName + " added from vending machine");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;

            if (!menuOpen && promptText != null)
                promptText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            menuOpen = false;

            if (promptText != null)
                promptText.SetActive(false);

            if (vendingPanel != null)
                vendingPanel.SetActive(false);
        }
    }
}