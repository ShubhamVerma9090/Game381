using UnityEngine;

public class VendingMachineInteract : MonoBehaviour
{
    [SerializeField] private GameObject vendingPanel;
    [SerializeField] private PlayerInventory playerInventory;

    private bool playerNear = false;

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            vendingPanel.SetActive(!vendingPanel.activeSelf);
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            vendingPanel.SetActive(false);
        }
    }
}