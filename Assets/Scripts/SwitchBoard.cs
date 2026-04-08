using UnityEngine;
using UnityEngine.UI;

public class SwitchBoard : MonoBehaviour
{
    public Image image;
    private bool isOn = false;
    private SwitchManager manager;

    private void Start()
    {
        image.color = Color.red;
        manager = GetComponentInParent<SwitchManager>();
    }

    public void Toggle()
    {
        // --- NEW LOGIC ---
        // If it's already ON, do nothing and exit the function.
        // This prevents the player from turning it back to red.
        if (isOn)
        {
            return;
        }
        // -----------------

        isOn = true; // Since we can't toggle back, we just set it to true
        image.color = Color.green;

        if (manager != null)
        {
            manager.ReportState(isOn);
        }
    }
}