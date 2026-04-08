using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> items = new List<string>();

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log("Picked up: " + itemName);
    }
}