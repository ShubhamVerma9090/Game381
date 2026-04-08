using UnityEngine;

public class SideScrollerFlashlight : MonoBehaviour
{
    void Update()
    {
        // 1. Get Mouse Position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Keep it in 2D space

        // 2. Get Direction from light to mouse
        Vector2 direction = mousePos - transform.position;

        // 3. Calculate Angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}