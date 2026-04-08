using UnityEngine;

public class BreakerInteract : MonoBehaviour
{

    public GameObject puzzleOverlay;

    void OnMouseDown()
    {
        if (puzzleOverlay != null)
        {
            puzzleOverlay.SetActive(true);

        }
    }
}