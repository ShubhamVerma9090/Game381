using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider Slider;
    public Image fillImage; // Drag the 'Fill' object here in the Inspector
    public Color Low = Color.red;
    public Color High = Color.green;
    public Vector3 offset = new Vector3(0, 2, 0);

    public void SetHealth(float health, float maxHealth)
    {
        Slider.gameObject.SetActive(health < maxHealth);
        Slider.value = health;
        Slider.maxValue = maxHealth;

        // Use the direct reference for better performance
        if (fillImage != null)
        {
            fillImage.color = Color.Lerp(Low, High, Slider.normalizedValue);
        }
    }

    void Update()
    {
        if (transform.parent != null)
        {
            // Position the health bar above the enemy's head
            Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
        }
    }
}