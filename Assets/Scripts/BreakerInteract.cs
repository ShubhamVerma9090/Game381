using UnityEngine;

public class BreakerInteract : MonoBehaviour
{
    public GameObject promptText;
    public GameObject puzzleOverlay;

    void OnMouseDown()
    {
        if (puzzleOverlay != null)
        {
            puzzleOverlay.SetActive(true);

        }
    }
}