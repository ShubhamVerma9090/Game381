using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject inventoryMenu;

    public bool IsOpen
    {
        get
        {
            return inventoryMenu != null && inventoryMenu.activeSelf;
        }
    }

    void Start()
    {
        if (inventoryMenu != null)
        {
            inventoryMenu.SetActive(false);
        }
    }

    public void OpenMenu()
    {
        if (inventoryMenu == null) return;

        inventoryMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        if (inventoryMenu == null) return;

        inventoryMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ToggleMenu()
    {
        if (IsOpen)
            CloseMenu();
        else
            OpenMenu();
    }

    void Update()
    {
        if (IsOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
    }
}